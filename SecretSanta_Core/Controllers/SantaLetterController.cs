using System.Text;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using secretsanta.Models;
using SecretSanta_Core.BusinessLogic;
using SecretSanta_Core.Data;
using SecretSanta_Core.Models;
using SecretSanta_Core.Repositories;
using SecretSanta_Core.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SecretSanta_Core.Controllers
{
    [Authorize(Roles = "Primary, Standard, Leader")]
    public class SantaLetterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly USPSAddressService _addressService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ExceptionUtilityService _exceptionUtilityService;
        private readonly EmailService _emailService;
        private readonly ExceptionUtilityService _logger;
        private readonly AgencyUserRepository _agencyUserRepository;

        public SantaLetterController(
            ApplicationDbContext context,
            USPSAddressService addressService,
            UserManager<ApplicationUser> userManager,
            ExceptionUtilityService exceptionUtilityService,
            EmailService emailService,
            AgencyUserRepository agencyUserRepository,
            ExceptionUtilityService logger)
        {
            _context = context;
            _addressService = addressService;
            _userManager = userManager;
            _exceptionUtilityService = exceptionUtilityService;
            _emailService = emailService;
            _logger = logger;
            _agencyUserRepository = agencyUserRepository;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetObject<LetterSantaModel>("LetterSanta") != null)
            {
                HttpContext.Session.Clear();
            }
            return View();
        }

        public IActionResult WriterInfo()
        {
            var agencies = _context.TblAgencies.Where(e => e.IsActive == true);
            ViewBag.Agencies = new SelectList(
                  agencies.Select(x => new { value = x.AgencyName, text = x.AgencyName }),
                   "value",
                   "text");
            if (HttpContext.Session.GetObject<LetterSantaModel>("LetterSanta") != null)
            {
                var letterSantaModel = HttpContext.Session.GetObject<LetterSantaModel>("LetterSanta");  
                if (letterSantaModel.WriterModel != null)
                {
                    var model = letterSantaModel.WriterModel;
                    return View(model);
                }
            }

            return View();
        }

        [HttpPost]
        public IActionResult WriterInfo(WriterInfoModel model)
        {
            var letterSantaModel = new LetterSantaModel
            {
                WriterModel = model
            };
            HttpContext.Session.SetObject("LetterSanta", letterSantaModel);
            return RedirectToAction("FamilyInfo");
        }

        public IActionResult FamilyInfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FamilyInfo(FamilyInfoModel model)
        {
            var letterSantaModel = HttpContext.Session.GetObject<LetterSantaModel>("LetterSanta");
            if (letterSantaModel == null)
            {
                return RedirectToAction("WriterInfo");
            }
            letterSantaModel.FamilyInfo = model;
            HttpContext.Session.SetObject("LetterSanta", letterSantaModel);
            return RedirectToAction("Info");
        }

        public ActionResult Info()
        {
            return View();
        }

        public ActionResult GuideMe()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GuideMe(GuideMeModel model)
        {
            var letterSantaModel = HttpContext.Session.GetObject<LetterSantaModel>("LetterSanta");
            var letter = SecretSantaLetter(model, letterSantaModel);
            HttpContext.Session.SetString("Letter", letter);

            return RedirectToAction("EditLetter");
        }

        public IActionResult AddLetter()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddLetter(LetterModel model)
        {
            return await SaveLetter(model);
        }

        private async Task<ActionResult> SaveLetter(LetterModel model)
        {
            var letterSantaModel = HttpContext.Session.GetObject<LetterSantaModel>("LetterSanta");
            letterSantaModel.Letter = model;
            var user = await _userManager.GetUserAsync(User);
            var agency = _context.TblAgencies.FirstOrDefault(a => a.AgencyId == user.AgencyId);
            letterSantaModel.Agency = agency.AgencyName;
            HttpContext.Session.SetObject("LetterSanta", letterSantaModel);

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                var letterId = SaveSantaLetter(HttpContext.Session.GetObject<LetterSantaModel>("LetterSanta"));
                var status = GenerateLetterStatus(letterId, (int)Enumerations.StatusTypes.Draft, user);
                _context.TblLetterStatuses.Add(status);
                scope.Complete();
                return RedirectToAction("FamilyMembers", new { id = letterId });
            }
            catch (Exception ex)
            {
                string input = JsonConvert.SerializeObject(model);
                var customException = new Exception(input, ex);
                _logger.LogException(customException);
                scope.Complete();
                throw;
            }
        }

        public IActionResult EditLetter()
        {
            var letter = HttpContext.Session.GetString("Letter");
            var model = new LetterModel { LetterText = letter };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditLetter(LetterModel model)
        {
            return await SaveLetter(model);
        }

        public IActionResult FamilyMembers(int id, int status = 0)
        {
            var familyMembers = _context.TblFamilyMembers.Where(f => f.LetterId == id);
            var model = familyMembers.Select(f => new FamilyMemberModel
            {
                Name = f.Name,
                Gender = f.Gender,
                Age = f.Age

            });
            ViewBag.LetterId = id;
            ViewBag.Status = status;
            return View(model);
        }

        public IActionResult AddFamilyMember(int id)
        {
            var model = new FamilyMemberModel
            {
                LetterId = id
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddFamilyMember(FamilyMemberModel model)
        {
            var familyMember = new TblFamilyMember
            {
                Name = model.Name,
                Age = model.Age.Value,
                AgeType = model.AgeType,
                Gender = model.Gender,
                WarmClothingSize = model.WarmClothingSize,
                WarmClothingType = model.WarmClothingType,
                ShoeSize = model.ShoeSize,
                ShoeSizeType = model.ShoeSizeType,
                Likes = model.Likes,
                OtherRequests = model.OtherRequests.SafeString(),
                LetterId = model.LetterId
            };

            _context.TblFamilyMembers.Add(familyMember);
            _context.SaveChanges();

            return RedirectToAction("FamilyMembers", new { id = model.LetterId, status = 1 });
        }

        public ActionResult Review(int id)
        {
            var model = GetModel(id);
            ViewData["Name"] = "Name";
            ViewData["Age"] = "Age";
            ViewData["Gender"] = "Gender";
            ViewData["WarmClothingSize"] = "Warm Clothing Size";
            ViewData["WarmClothingSizeInfo"] = "Enter a number or a size like med. Be sure to select the next field also.";
            ViewData["Size"] = "Size";
            ViewData["ShoeSize"] = "Shoe Size";
            ViewData["Likes"] = "Interest / Hobbies / Likes";
            ViewData["OtherRequests"] = "Other Requests";
            ViewData["FamilyMemberWarning"] = "Name, Age, Gender, Warm Clothing Size, Shoe Size, and Likes are required for each Family Member.";
            ViewData["FamilyCountWarning"] = "Must have at least one family member.";
            return View(model);
        }

        public ActionResult Print(int? id)
        {
            ReviewModel model;
            if (id.HasValue)
            {
                model = GetModel(id.Value);
                model.FamilyMembers = _context.TblFamilyMembers.Where(m => m.LetterId == id).Select(
                    f => new FamilyMemberModel
                    {
                        Age = f.Age,
                        Name = f.Name,
                        Gender = f.Gender,
                        WarmClothingSize = f.WarmClothingSize,
                        ShoeSize = f.ShoeSize,
                        Likes = f.Likes,
                        OtherRequests = f.OtherRequests,
                        WarmClothingType = f.WarmClothingType,
                        ShoeSizeType = f.ShoeSizeType
                    }).ToArray();
            }
            else
            {
                model = HttpContext.Session.GetObject<ReviewModel>("CompletedApplication");
            }

            ViewBag.LetterPrint = "<p>" + GetPrint(model) + "<p>";
            ViewBag.Print = "Print";
            return View();
        }

        public ActionResult Disclaimer()
        {
            return View();
        }

        private string SecretSantaLetter(GuideMeModel model, LetterSantaModel letterSantaModel)
        {
            var sb = new StringBuilder();
            sb.Append("My name is " + letterSantaModel.WriterModel.FirstName + " and I live in " + letterSantaModel.WriterModel.City + ".\r\n\r\n");
            sb.Append("I am writing to you today because my family has a problem. " + model.Problem + "\r\n\r\n");
            sb.Append("We are wishing that Secret Santa can help us out by granting this wish. " + model.Need + "\r\n\r\n");
            sb.Append("If we were able to get this help from Secret Santa it would make a difference to us. " + model.Benefit + "\r\n\r\n");
            sb.Append("Thank you very much and I wish you the best for the holiday season.");
            return sb.ToString();
        }

        private int SaveSantaLetter(LetterSantaModel model)
        {
            var santaLetter = new TblLetterSanta
            {
                WriterName = model.WriterModel.WriterName,
                FirstName = model.WriterModel.FirstName,
                LastName = model.WriterModel.LastName,
                Address = model.WriterModel.Address,
                City = model.WriterModel.City,
                Zip = model.WriterModel.Zip,
                Agency = model.WriterModel.Agency,
                NumChildrenUnder19 = model.FamilyInfo.NumChildrenUnder19.SafeInt(),
                NumChildrenOver19 = model.FamilyInfo.NumChildrenOver19.SafeInt(),
                NumParents = model.FamilyInfo.NumParents.SafeInt(),
                NumGrandparents = model.FamilyInfo.NumGrandparents.SafeInt(),
                NumOtherFamily = model.FamilyInfo.NumOtherFamily.SafeInt(),
                Letter = model.Letter.LetterText,
                Phone = model.WriterModel.Phone,
                Email = model.WriterModel.Email
            };

            _context.TblLetterSanta.Add(santaLetter);
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

            var letterId = santaLetter.LetterId;
            return letterId;
        }

        private string GetPrint(ReviewModel model)
        {
            var sb = new StringBuilder();
            sb.Append("<table border='1'");
            sb.Append($"<tr><td>First Name</td><td>{model.FirstName}</td></tr>");
            sb.Append($"<tr><td>Last Name</td><td>{model.FirstName}</td></tr>");
            sb.Append($"<tr><td>Address</td><td>{model.Address}</td></tr>");
            sb.Append($"<tr><td>City</td><td>{model.City}</td></tr>");
            sb.Append($"<tr><td>Zip</td><td>{model.Zip}</td></tr>");
            sb.Append($"<tr><td>Phone</td><td>{model.Phone}</td></tr>");
            sb.Append($"<tr><td>Email</td><td>{model.Email}</td></tr>");
            sb.Append($"<tr><td>Number of Children Under 19</td><td>{model.NumChildrenUnder19}</td></tr>");
            sb.Append($"<tr><td>Number of Children Age 19 or Older</td><td>{model.NumChildrenOver19}</td></tr>");
            sb.Append($"<tr><td>Number of Parents</td><td>{model.NumParents}</td></tr>");
            sb.Append($"<tr><td>Number of Grandparents</td><td>{model.NumGrandparents}</td></tr>");
            sb.Append($"<tr><td>Number of Other Family Members</td><td>{model.NumOtherFamily}</td></tr>");
            sb.Append($"<tr><td>Letter</td><td>{model.Letter}</td></tr>");
            if (model.FamilyMembers != null)
            {
                foreach (var familyMember in model.FamilyMembers)
                {
                    var otherRequests = familyMember.OtherRequests != null ? familyMember.OtherRequests : String.Empty;
                    sb.Append($"<tr><td>Name</td><td>{familyMember.Name}</td></tr>");
                    sb.Append($"<tr><td>Age</td><td>{familyMember.Age}</td></tr>");
                    sb.Append($"<tr><td>Gender</td><td>{familyMember.Gender}</td></tr>");
                    sb.Append($"<tr><td>Warm Clothing Size</td><td>{familyMember.WarmClothingSize}</td></tr>");
                    sb.Append($"<tr><td>&nbsp;</td><td>{familyMember.WarmClothingType}</td></tr>");
                    sb.Append($"<tr><td>Shoe Size</td><td>{familyMember.ShoeSize}</td></tr>");
                    sb.Append($"<tr><td>&nbsp;</td><td>{familyMember.ShoeSizeType}</td></tr>");
                    sb.Append($"<tr><td>Interest / Hobbies / Likes</td><td>{familyMember.Likes}</td></tr>");
                    sb.Append($"<tr><td>Other Requests</td><td>{otherRequests}</td></tr>");
                }
            }

            sb.Append("</table>");
            return sb.ToString();
        }

        public JsonResult SaveReview(ReviewModel model)
        {
            HttpContext.Session.SetObject("CompletedApplication", model);
            return Json(new { message = String.Empty });

        }

        public JsonResult SaveLetter()
        {
            var model = HttpContext.Session.GetObject<ReviewModel>("CompletedApplication");

            var letterSanta = _context.TblLetterSanta.FirstOrDefault(l => l.LetterId == model.LetterId);

            letterSanta.Address = model.Address;
            letterSanta.City = model.City;
            letterSanta.Zip = model.Zip;
            letterSanta.Phone = model.Phone;
            letterSanta.Email = model.Email;
            letterSanta.NumChildrenUnder19 = model.NumChildrenUnder19;
            letterSanta.NumChildrenOver19 = model.NumChildrenOver19;
            letterSanta.NumParents = model.NumParents;
            letterSanta.NumGrandparents = model.NumGrandparents;
            letterSanta.NumOtherFamily = model.NumOtherFamily;
            letterSanta.Letter = model.Letter;
            letterSanta.FirstName = model.FirstName;
            letterSanta.LastName = model.LastName;
            letterSanta.WriterName = model.WriterName;
            letterSanta.IsActive = true;

            _context.TblLetterSanta.Update(letterSanta);

            if (model.FamilyMembers != null)
            {
                foreach (var familyMember in model.FamilyMembers)
                {
                    if (familyMember.FamilyMemberId > 0)
                    {
                        var member = _context.TblFamilyMembers.FirstOrDefault(m => m.FamilyMemberId == familyMember.FamilyMemberId);
                        member.Name = familyMember.Name;
                        member.Age = familyMember.Age.HasValue ? familyMember.Age.Value : 0;
                        member.Gender = familyMember.Gender;
                        member.WarmClothingSize = familyMember.WarmClothingSize;
                        member.WarmClothingType = familyMember.WarmClothingType;
                        member.ShoeSize = familyMember.ShoeSize;
                        member.ShoeSizeType = familyMember.ShoeSizeType;
                        member.Likes = familyMember.Likes;
                        member.OtherRequests = familyMember.OtherRequests;
                        member.IsActive = true;

                        _context.TblFamilyMembers.Update(member);

                    }
                    else
                    {
                        var add = new TblFamilyMember
                        {
                            Name = familyMember.Name,
                            Age = familyMember.Age.HasValue ? familyMember.Age.Value : 0,
                            Gender = familyMember.Gender,
                            WarmClothingSize = familyMember.WarmClothingSize,
                            WarmClothingType = familyMember.WarmClothingType,
                            ShoeSize = familyMember.ShoeSize,
                            ShoeSizeType = familyMember.ShoeSizeType,
                            Likes = familyMember.Likes,
                            OtherRequests = familyMember.OtherRequests,
                            LetterId = model.LetterId,
                            IsActive = true
                        };

                        _context.TblFamilyMembers.Add(add);

                    }
                }
            }

            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            var status = GenerateLetterStatus(model.LetterId, (int)Enumerations.StatusTypes.New, user);
            _context.TblLetterStatuses.Add(status);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _exceptionUtilityService.LogException(ex);
                throw;
            }

            SendConfirmationEmail(model.Email, model.LetterId).GetAwaiter();

            return Json(new { message = String.Empty });
        }

        private async Task SendConfirmationEmail(string emailTo, int letterId)
        {
            var subject = "Santa Letter Received";
            var body = String.Empty;

            var adminEmail = _context.TblAdminEmails.FirstOrDefault(l => l.EmailName == "Santa Letter Received English");
            body = String.Format(adminEmail.EmailContent, new object[]
            {
                letterId
            });

            var attachment = GetConfirmationAttachment(letterId);

            var emailServiceModel = new EmailServiceModel
            {
                To = emailTo,
                Subject = subject,
                Message = body,
                AttachmentFileName = "Letter.txt",
                AttachmentContentType = "text/plain",
                AttachmentContent = attachment,
                HasAttachment = true
            };

            await _emailService.SendEmail(emailServiceModel);

        }

        private string GetConfirmationAttachment(int id)
        {
            var letter = _context.TblLetterSanta.FirstOrDefault(l => l.LetterId == id);

            var sb = new StringBuilder();
            sb.AppendLine($" Letter #{letter.LetterId}");
            sb.AppendLine(letter.Letter);
            return sb.ToString();

        }

        private TblLetterStatus GenerateLetterStatus(int letterId, int statusId, ApplicationUser user)
        {
            var status = new TblLetterStatus
            {
                LetterId = letterId,
                StatusId = statusId,
                DateEdited = BusinessMethods.GetLocalDateTime(DateTime.UtcNow),
                EditedByUser = user.Id
            };

            return status;
        }

        private ReviewModel GetModel(int id)
        {
            var santaLetter = _context.TblLetterSanta.FirstOrDefault(l => l.LetterId == id);
            var model = new ReviewModel
            {
                LetterId = santaLetter.LetterId,
                Address = santaLetter.Address,
                City = santaLetter.City,
                Zip = santaLetter.Zip,
                NumChildrenUnder19 = santaLetter.NumChildrenUnder19,
                NumChildrenOver19 = santaLetter.NumChildrenOver19,
                NumParents = santaLetter.NumParents,
                NumGrandparents = santaLetter.NumGrandparents,
                Letter = santaLetter.Letter,
                FirstName = santaLetter.FirstName,
                LastName = santaLetter.LastName,
                WriterName = santaLetter.WriterName,
                NumOtherFamily = santaLetter.NumOtherFamily,
                Phone = santaLetter.Phone,
                Email = santaLetter.Email,
                Agency = santaLetter.Agency
            };
            return model;
        }

        public JsonResult GetFamilyMembers(int letterId)
        {
            var familyMembers = _context.TblFamilyMembers.Where(m => m.LetterId == letterId).ToList();
            var members = familyMembers.Select(f => new FamilyMemberModel
            {
                FamilyMemberId = f.FamilyMemberId,
                Age = f.Age,
                Name = f.Name,
                Gender = f.Gender,
                WarmClothingSize = f.WarmClothingSize,
                WarmClothingType = f.WarmClothingType,
                ShoeSize = f.ShoeSize,
                ShoeSizeType = f.ShoeSizeType,
                Likes = f.Likes,
                OtherRequests = f.OtherRequests,
                LetterId = f.LetterId

            });
            return Json(new { familyMembers = members });

        }

        public JsonResult VerifyAddress(AddressModel model)
        {
            var suggestedAddress = new AddressModel();
            try
            {
                suggestedAddress = _addressService.VerifyAddress(model);
            }
            catch (Exception ex)
            {
                string input = JsonConvert.SerializeObject(model);
                var customException = new Exception(input, ex);
                _logger.LogException(customException);
                throw;
            }

            if (!String.IsNullOrEmpty(suggestedAddress.Address) && !String.IsNullOrEmpty(suggestedAddress.City) &&
                !string.IsNullOrEmpty(suggestedAddress.Zip))
            {
                if (suggestedAddress.Address != model.Address || suggestedAddress.City != model.City ||
                    suggestedAddress.Zip != model.Zip)
                {
                    return Json(new { status = "suggested", suggestedAddress });
                }
            }

            return Json(new { status = "none" });
        }

    }
}
