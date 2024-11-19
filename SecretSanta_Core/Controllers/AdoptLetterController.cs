using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SecretSanta_Core.BusinessLogic;
using SecretSanta_Core.Data;
using SecretSanta_Core.Models;
using SecretSanta_Core.Repositories;
using Stripe;
using System.Diagnostics;
using System.Net;
using System.Text;
using SecretSanta_Core.Services;
using Newtonsoft.Json;

namespace SecretSanta_Core.Controllers
{
    public class AdoptLetterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly LetterSantaRepository _letterSantaRepository;
        private readonly AppSettings _appSettings;
        private readonly EmailService _emailService;
        private readonly ExceptionUtilityService _logger;

        public AdoptLetterController(ApplicationDbContext context, LetterSantaRepository letterSantaRepository, IOptions<AppSettings> appSettings,
            EmailService emailService, ExceptionUtilityService logger)
        {
            _context = context;
            _letterSantaRepository = letterSantaRepository;
            _appSettings = appSettings.Value;
            _emailService = emailService;
            _logger = logger;

        }

        public IActionResult Index()
        {
            var letters = _letterSantaRepository.GetLetters((int)Enumerations.StatusTypes.LetterHomePost);
            var model = letters.Select(l => new AdoptLetterModel
            {
                LetterId = l.LetterId,
                LetterSummary = l.LetterSummary
            }).ToList();
            ViewBag.GiftDueDate = _context.ApplicationSettings.FirstOrDefault(s => s.SettingsName == "Gift Due Date")
                .SettingsValue;
            return View(model);
        }

        public ActionResult Letter(int id)
        {
            var letter = _context.TblLetterSanta.FirstOrDefault(l => l.LetterId == id);
            var model = new AdoptLetterModel
            {
                LetterId = letter.LetterId,
                LetterSummary = letter.LetterSummary,
                Letter = BusinessMethods.RTFToHtml(letter.Letter)
            };
            var familyMembers = _context.TblFamilyMembers.Where(m => m.LetterId == id && m.IsActive).ToList();
            var members = new List<FamilyMemberModel>();
            foreach (var m in familyMembers)
            {
                var mdl = new FamilyMemberModel
                {
                    InfoString = GetFamilyMemberInfoString(m)
                };
                members.Add(mdl);
            }

            model.FamilyMembers = members;
            HttpContext.Session.SetObject("Letter", model);
            ViewBag.ContactEmail = _appSettings.ContactEmail;
            return View(model);
        }

        private string GetFamilyMemberInfoString(TblFamilyMember m)
        {
            if (String.IsNullOrEmpty(m.OtherRequests))
            {
                return
                    $"Age {m.Age}, Gender {m.Gender}, Warm Clothing {m.WarmClothingSize} {m.WarmClothingType}, Shoe {m.ShoeSize} {m.ShoeSizeType}, Likes {m.Likes}";
            }
            return
                $"Age {m.Age}, Gender {m.Gender}, Warm Clothing {m.WarmClothingSize} {m.WarmClothingType}, Shoe {m.ShoeSize} {m.ShoeSizeType}, Likes {m.Likes}, {m.OtherRequests}";
        }

        [HttpPost]
        public ActionResult Letter(AdoptLetterModel model)
        {
            if (model.GiftMethod == "Gift")
            {
                var letter = ProcessAdoption(model);
                SendConfirmationEmail(letter).GetAwaiter();
                return RedirectToAction("Index", "Home");
            }

            HttpContext.Session.SetObject("AdoptLetterModel", model);
            return RedirectToAction("Payment");
        }

        private TblLetterSanta ProcessAdoption(AdoptLetterModel model)
        {
            var letter = _context.TblLetterSanta.FirstOrDefault(l => l.LetterId == model.LetterId);

            letter.AdoptedBy = model.AdoptedBy;
            letter.AdoptedByEmail = model.AdoptedByEmail;
            letter.AdoptedByPhone = model.AdoptedByPhone;
            _context.TblLetterSanta.Update(letter);

            var status = GenerateLetterStatus(model.LetterId, (int)Enumerations.StatusTypes.LetterHomeAdopt);
            _context.TblLetterStatuses.Add(status);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string input = JsonConvert.SerializeObject(model);
                var customException = new Exception(input, ex);
                _logger.LogException(customException);
                throw;
            }

            return letter;
        }

        public ActionResult Payment()
        {
            var model = new PaymentModel()
            {
                StripePublishableKey = _appSettings.StripeAPIKey,
            };
            ViewBag.Amount = "100.00";
            return View(model);
        }

        [HttpPost]
        public ActionResult Payment(PaymentModel model)
        {
            var letter = HttpContext.Session.GetObject<AdoptLetterModel>("AdoptLetterModel");
            var email = letter.ConfirmEmail;

            var customers = new CustomerService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = email,
                Source = model.StripeToken
            });

            var chargeService = new ChargeService();

            var chargeOptions = new ChargeCreateOptions()
            {
                Amount = (int)(model.Amount * 100),
                Currency = "usd",
                Customer = customer.Id,
                Description = string.Format($"Secret Santa Donation for Santa Letter {letter.LetterId}"),
                ReceiptEmail = email
            };

            try
            {
                var stripeCharge = chargeService.Create(chargeOptions);
            }
            catch (StripeException stripeException)
            {
                Debug.WriteLine(stripeException.Message);
                ModelState.AddModelError(string.Empty, stripeException.Message);
                return View(model);
            }

            ProcessAdoption(letter);
            SendDonationEmail(email, model.Amount, $"Letter #{letter.LetterId}").GetAwaiter();
            return RedirectToAction("PaymentConfirmation");
        }

        public ActionResult PaymentConfirmation()
        {
            return View();
        }

        private async Task SendConfirmationEmail(TblLetterSanta letter)
        {
            var adminEmail = _context.TblAdminEmails.FirstOrDefault(l => l.EmailName == "Adopt Letter Confirmation");
            var body = String.Format(adminEmail.EmailContent, new object[]
            {
                letter.LetterId
            });

            var attachment = GetConfirmationAttachment(letter);

            var emailServiceModel = new EmailServiceModel
            {
                To = letter.AdoptedByEmail,
                Subject = "Adopt a Letter Confirmation",
                Message = body,
                HasAttachment = true,
                CC = _appSettings.AdminEmail,
                AttachmentFileName = "Letter.txt",
                AttachmentContentType = "text/plain",
                AttachmentContent = attachment,
            };

            await _emailService.SendEmail(emailServiceModel);

        }


        private string GetConfirmationAttachment(TblLetterSanta letter)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"# {letter.LetterId} {letter.LetterSummary}");
            sb.AppendLine(letter.Letter);

            var familyMembers = _context.TblFamilyMembers.Where(m => m.LetterId == letter.LetterId).ToList();

            if (familyMembers.Any())
            {
                sb.AppendLine("Family Members");
                foreach (var m in familyMembers)
                {

                    sb.AppendLine(GetFamilyMemberInfoString(m));

                }
            }
            return sb.ToString();
        }

        private async Task SendDonationEmail(string emailTo, decimal amount, string receiptNumber)
        {
            var adminEmail = _context.TblAdminEmails.FirstOrDefault(l => l.EmailName == "Donation Email");
            var body = String.Format(adminEmail.EmailContent, new object[]
            {
                emailTo, amount, receiptNumber
            });

            var emailServiceModel = new EmailServiceModel
            {
                To = emailTo,
                Subject = "Donation Received",
                Message = body,
                HasAttachment = false,
                CC = _appSettings.AdminEmail
            };

            await _emailService.SendEmail(emailServiceModel);
        }


        public ActionResult Print()
        {
            var model = HttpContext.Session.GetObject<AdoptLetterModel>("Letter");
            return View(model);
        }

        private TblLetterStatus GenerateLetterStatus(int letterId, int statusId)
        {
            var status = new TblLetterStatus
            {
                LetterId = letterId,
                StatusId = statusId,
                DateEdited = BusinessMethods.GetLocalDateTime(DateTime.UtcNow)
            };

            return status;
        }
    }
}
