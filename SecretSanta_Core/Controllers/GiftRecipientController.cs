using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecretSanta_Core.BusinessLogic;
using SecretSanta_Core.Data;
using SecretSanta_Core.Models;
using SecretSanta_Core.Repositories;
using SecretSanta_Core.Enumerations;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using SecretSanta_Core.Services;

namespace SecretSanta_Core.Controllers
{

    [Authorize(Roles = "Primary, Leader")]
    public class GiftRecipientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DashboardCountRepository _dashboardCountRepository;
        private readonly RecipientViewRepository _recipientViewRepository;
        private readonly GiftViewRepository _giftViewRepository;
        private readonly ThankYouRepository _thankYouRepository;
        private readonly StorageService _storageService;
        private readonly ExceptionUtilityService _logger;

        public GiftRecipientController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            DashboardCountRepository dashboardCountRepository,
            RecipientViewRepository recipientViewRepository, GiftViewRepository giftViewRepository,
            ThankYouRepository thankYouRepository, StorageService storageService,
            ExceptionUtilityService logger)
        {
            _context = context;
            _userManager = userManager;
            _dashboardCountRepository = dashboardCountRepository;
            _recipientViewRepository = recipientViewRepository;
            _giftViewRepository = giftViewRepository;
            _thankYouRepository = thankYouRepository;
            _storageService = storageService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var agencyId = user.AgencyId.Value;

            var model = new DashboardModel();

            var counts = _dashboardCountRepository.CountStatus(agencyId);
            model.NumPending = counts.FirstOrDefault(c => c.WebGroup == "Pending") != null
                ? counts.FirstOrDefault(c => c.WebGroup == "Pending").Count
                : 0;
            model.NumCancel = counts.FirstOrDefault(c => c.WebGroup == "Cancel") != null
                ? counts.FirstOrDefault(c => c.WebGroup == "Cancel").Count
                : 0;
            model.NumWebAdopt = counts.FirstOrDefault(c => c.WebGroup == "WebAdopt") != null
                ? counts.FirstOrDefault(c => c.WebGroup == "WebAdopt").Count
                : 0; //Web Adopt
            model.NumIn = counts.FirstOrDefault(c => c.WebGroup == "FLOOR GIFTS IN") != null
                ? counts.FirstOrDefault(c => c.WebGroup == "FLOOR GIFTS IN").Count
                : 0; //In
            model.NumInBike = counts.FirstOrDefault(c => c.WebGroup == "BIKES IN") != null
                ? counts.FirstOrDefault(c => c.WebGroup == "BIKES IN").Count
                : 0; //In Bike
            model.NumInGifCard = counts.FirstOrDefault(c => c.WebGroup == "GIFT CARDS IN") != null
                ? counts.FirstOrDefault(c => c.WebGroup == "GIFT CARDS IN").Count
                : 0; //In GifCard
            model.NumOut = counts.FirstOrDefault(c => c.WebGroup == "Out") != null
                ? counts.FirstOrDefault(c => c.WebGroup == "Out").Count
                : 0; //Out
            model.NumOutBike = counts.FirstOrDefault(c => c.WebGroup == "OutBike") != null
                ? counts.FirstOrDefault(c => c.WebGroup == "OutBike").Count
                : 0; //Out Bike
            model.NumOutGfCard = counts.FirstOrDefault(c => c.WebGroup == "OutGfCard") != null
                ? counts.FirstOrDefault(c => c.WebGroup == "OutGfCard").Count
                : 0; //Out GfCard
            model.NumOther = counts.FirstOrDefault(c => c.WebGroup == "Other") != null
                ? counts.FirstOrDefault(c => c.WebGroup == "Other").Count
                : 0; //Other

            var recipientCounts = _dashboardCountRepository.CountRecipientStatus(agencyId);
			model.NumInActive = 0;
            model.NumDraft = recipientCounts.FirstOrDefault(c => c.WebGroup == "Draft") != null
                ? recipientCounts.FirstOrDefault(c => c.WebGroup == "Draft").Count
                : 0;
			model.NumInActive += model.NumDraft;
			model.NumNew = recipientCounts.FirstOrDefault(c => c.WebGroup == "New") != null
                ? recipientCounts.FirstOrDefault(c => c.WebGroup == "New").Count
                : 0;
			model.NumInActive += model.NumNew;
			model.NumApproved = recipientCounts.FirstOrDefault(c => c.WebGroup == "Approved") != null
                ? recipientCounts.FirstOrDefault(c => c.WebGroup == "Approved").Count
                : 0;
			model.NumInActive += model.NumApproved;
			model.NumRevise = recipientCounts.FirstOrDefault(c => c.WebGroup == "Revise") != null
                ? recipientCounts.FirstOrDefault(c => c.WebGroup == "Revise").Count
                : 0;
			model.NumInActive += model.NumRevise;
			model.NumActive = recipientCounts.FirstOrDefault(c => c.WebGroup == "Active") != null
                ? recipientCounts.FirstOrDefault(c => c.WebGroup == "Active").Count
                : 0;
			
			model.NumRecipientCancel = recipientCounts.FirstOrDefault(c => c.WebGroup == "Cancel") != null
                ? counts.FirstOrDefault(c => c.WebGroup == "Cancel").Count
                : 0;
            model.NumInActive += model.NumRecipientCancel;

			model.NumRecipientOther = recipientCounts.FirstOrDefault(c => c.WebGroup == "Other") != null
                ? recipientCounts.FirstOrDefault(c => c.WebGroup == "Other").Count
                : 0; //Other
			model.NumInActive += model.NumRecipientOther;

			var agencyName = _context.TblAgencies.ToList().FirstOrDefault(a => a.AgencyId == user.AgencyId).AgencyName;
            ViewBag.AgencyName = agencyName;
            ViewBag.Attention = model.NumDraft + model.NumRevise > 0;
            ViewBag.AgencyId = user.AgencyId;
            return View(model);
        }

        public async Task<ActionResult> Add()
        {
            var model = new GiftRecipientModel();
            model.AgeType = "years";
            var giftWishTypes = _context.GiftDetails.OrderBy(g => g.GiftDetailOrder)
                .Select(g => new { Id = g.GiftDetailId, Text = g.GiftDetailText })
                .ToDictionary(x => x.Id, x => x.Text);
            ViewBag.GiftTypes = new SelectList(
                ((Dictionary<int, string>)giftWishTypes).Select(x => new { value = x.Key, text = x.Value }),
                "value", 
                "text");
            var user = await _userManager.GetUserAsync(User);
            var agency = _context.TblAgencies.FirstOrDefault(a => a.AgencyId == user.AgencyId);
            ViewBag.AgencyID = agency.AgencyId;
            ViewBag.AgencyName = agency.AgencyName;
            var locations =
                _context.TblAgencyLocations.Where(l => l.AgencyId == agency.AgencyId)
                    .Select(x => new { Id = x.Location, Name = x.Location })
                    .ToDictionary(x => x.Id, x => x.Name);
            if (locations.Any())
            {
                ViewBag.HasLocations = "True";
                ViewBag.Locations = new SelectList(
                    ((Dictionary<string, string>)locations).Select(x => new { value = x.Key, text = x.Value }),
                    "value", 
                    "text");
            }
            else
            {
                ViewBag.HasLocations = "False";
            }

            ViewBag.Year = BusinessMethods.GetLocalDateTime(DateTime.UtcNow).Year;

            return View(model);
        }


        public JsonResult GetGiftDetails()
        {
            var details = _context.GiftDetails;
            var giftDetails = details.Select(g => new
            {
                giftDetailId = g.GiftDetailId,
                giftIdeaDescription = g.GiftIdeaDescription,
                giftDetailText = g.GiftDetailText,
                lblGiftDetail1 = g.LblGiftDetail1,
                lblGiftDetail2 = g.LblGiftDetail2
            }).ToList();
            return Json(new { result = giftDetails });
        }

        public async Task<JsonResult> AddGiftRecipient(GiftRecipientModel model)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var recipient = await GetRecipient(model);
                recipient.IsActive = true;
                _context.TblRecipientParents.Add(recipient);
                _context.SaveChanges();

                var recipientNumber = recipient.RecipientNum;
                var child = new TblRecipientChild
                {
                    RecipientNum = recipientNumber,
                    Primary = true
                };
                _context.TblRecipientChildren.Add(child);
                _context.SaveChanges();

                var labelNum = child.LabelNum;
                
                var status = GenerateStatusLog(labelNum, Convert.ToInt32(model.Status));

                _context.TblStatusLogs.Add(status);
                _context.SaveChanges();
                scope.Complete();
            }
            catch (Exception ex)
            {
                string input = JsonConvert.SerializeObject(model);
                var customException = new Exception(input, ex);
                _logger.LogException(customException);
                scope.Dispose();
                throw;
            }

            return Json(new { message = "OK" });

        }

        public async Task<ActionResult> Edit(int id)
        {
            var recipient = _context.TblRecipientParents.FirstOrDefault(p => p.RecipientNum == id);
            var model = new GiftRecipientModel()
            {
                RecipientNum = recipient.RecipientNum,
                Location = recipient.Location,
                Name = recipient.Name,
                Age = recipient.Age,
                AgeType = recipient.AgeType,
                Gender = recipient.Gender,
                RecipientInfo = recipient.RecipientInfo,
                GiftWish = recipient.GiftWish,
                GiftType = recipient.GiftType,
                GiftDetail1 = recipient.GiftDetail1,
                GiftDetail2 = recipient.GiftDetail2,
                AltGiftWish = recipient.AltGiftWish,
                AltGiftType = recipient.AltGiftType,
                AltGiftDetail1 = recipient.AltGiftDetail1,
                AltGiftDetail2 = recipient.AltGiftDetail2

            };
            var giftWishTypes = _context.GiftDetails.OrderBy(g => g.GiftDetailOrder).Select(g => new { Id = g.GiftDetailId, Text = g.GiftDetailText })
                .ToDictionary(x => x.Id, x => x.Text);
            ViewBag.GiftTypes = new SelectList(
                ((Dictionary<int, string>)giftWishTypes).Select(x => new { value = x.Key, text = x.Value }),
                "value", "text");
            var user = await _userManager.GetUserAsync(User);
            var agency = _context.TblAgencies.FirstOrDefault(a => a.AgencyId == user.AgencyId);
            ViewBag.AgencyID = agency.AgencyId;
            ViewBag.AgencyName = agency.AgencyName;
            var locations =
                _context.TblAgencyLocations.Where(l => l.AgencyId == agency.AgencyId).Select(x => new { Id = x.Location, Name = x.Location })
                    .ToDictionary(x => x.Id, x => x.Name);
            if (locations.Any())
            {
                ViewBag.HasLocations = "True";
                ViewBag.Locations = new SelectList(
                    ((Dictionary<string, string>)locations).Select(x => new { value = x.Key, text = x.Value }),
                    "value", "text");
            }
            else
            {
                ViewBag.HasLocations = "False";
            }

            ViewBag.Year = BusinessMethods.GetLocalDateTime(DateTime.UtcNow).Year;

            return View(model);
        }

        public JsonResult EditGiftRecipient(GiftRecipientModel model)
        {
            var existingRecipient =
                _context.TblRecipientParents.FirstOrDefault(r => r.RecipientNum == model.RecipientNum);
            existingRecipient.Location = model.Location;
            existingRecipient.Name = model.Name;
            existingRecipient.Age = model.Age;
            existingRecipient.AgeType = model.AgeType;
            existingRecipient.Gender = model.Gender;
            existingRecipient.RecipientInfo = model.RecipientInfo;
            existingRecipient.GiftWish = model.GiftWish;
            existingRecipient.GiftType = model.GiftType;
            existingRecipient.GiftDetail1 = model.GiftDetail1;
            existingRecipient.AltGiftDetail2 = model.AltGiftDetail2;
            existingRecipient.AltGiftWish = model.AltGiftWish;
            existingRecipient.AltGiftType = model.AltGiftType;
            existingRecipient.AltGiftDetail1 = model.AltGiftDetail1;
            existingRecipient.AltGiftDetail2 = model.AltGiftDetail2;
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                _context.TblRecipientParents.Update(existingRecipient);

                var recipientChild = _context.TblRecipientChildren.FirstOrDefault(c => c.RecipientNum == model.RecipientNum);
                var labelNum = recipientChild.LabelNum;
                var currentStatus = _context.TblStatusLogs.Where(l => l.LabelNum == labelNum)
                    .OrderByDescending(l => l.LogId).First();
                var statusId = currentStatus.StatusId;
                TblStatusLog status = new TblStatusLog();
                if (statusId == (int)Enumerations.StatusTypes.Draft || statusId == (int)Enumerations.StatusTypes.New)
                {
                    status = GenerateStatusLog(labelNum, Convert.ToInt32(model.Status));
                    _context.TblStatusLogs.Add(status);
                }
                else if (statusId == (int)Enumerations.StatusTypes.Edit)
                {
                    if (Convert.ToInt32(model.Status) == (int)Enumerations.StatusTypes.New)
                    {
                        status = GenerateStatusLog(labelNum, (int)Enumerations.StatusTypes.Edited);
                    }
                    else if (Convert.ToInt32(model.Status) == (int)Enumerations.StatusTypes.Draft)
                    {
                        status = GenerateStatusLog(labelNum, (int)Enumerations.StatusTypes.Edit);
                    }

                    _context.TblStatusLogs.Add(status);
                }

                _context.SaveChanges();
                scope.Complete();

            }
            catch (Exception ex)
            {
                string input = JsonConvert.SerializeObject(model);
                var customException = new Exception(input, ex);
                _logger.LogException(customException);
                scope.Dispose();
                throw;
            }

            return Json(new { message = "OK" });

        }

        public JsonResult GetRecipientsByStatus(string webGroups, int agencyId)
        {
            var dashboardGroups = JsonConvert.DeserializeObject<List<int>>(webGroups);
            var rcpts = _recipientViewRepository.GetRecipientsByStatus(dashboardGroups, agencyId).OrderByDescending(g => g.RecipientNum);

            var recipients = rcpts.Select(r => new
            {
                age = r.Age,
                ageType = r.AgeType,
                gender = r.Gender,
                recipientInfo = r.RecipientInfo,
                giftWish = r.GiftWish,
                altGiftWish = r.AltGiftWish,
                editNotes = r.EditNotes,
                recipientNum = r.RecipientNum,
                name = r.Name,
                status = r.Status
            });
            return Json(new { result = recipients });
        }

        public JsonResult GetGiftsByStatus(string webGroups, int agencyId)
        {
            var dashboardGroups = JsonConvert.DeserializeObject<List<int>>(webGroups);
            var gfts = _giftViewRepository.GetGiftsByStatus(dashboardGroups, agencyId).OrderByDescending(g => g.RecipientNum);

            var gifts = gfts.Select(g => new
            {
                age = g.Age,
                ageType = g.AgeType,
                gender = g.Gender,
                recipientInfo = g.RecipientInfo,
                giftWish = g.GiftWish,
                altGiftWish = g.AltGiftWish,
                editNotes = g.EditNotes,
                location = g.Location,
                recipientNum = g.RecipientNum,
                labelNum = g.LabelNum,
                name = g.Name,
                status = g.Status
            });
            return Json(new { gifts = gifts });

        }
        public JsonResult GetDraftEdit(int agencyId)
        {
            var webGroups = new List<int> { (int)DashboardTypes.Draft, (int)DashboardTypes.Revise };
            var rcpts = _recipientViewRepository.GetRecipientsByStatus(webGroups, agencyId).OrderByDescending(r => r.RecipientNum);
            var recipients = rcpts.Select(r => new
            {
                age = r.Age,
                ageType = r.AgeType,
                gender = r.Gender,
                recipientInfo = r.RecipientInfo,
                giftWish = r.GiftWish,
                altGiftWish = r.AltGiftWish,
                editNotes = r.EditNotes,
                recipientNum = r.RecipientNum,
                name = r.Name,
                status = r.Status
            });
            return Json(new { result = recipients });
        }


        public JsonResult GetAllGiftsForAgency(int agencyId)
        {
            var gfts = _giftViewRepository.GetGiftsForAgency(agencyId).OrderByDescending(g => g.RecipientNum);

            var gifts = gfts.Select(g => new
            {
                age = g.Age,
                ageType = g.AgeType,
                gender = g.Gender,
                recipientInfo = g.RecipientInfo,
                giftWish = g.GiftWish,
                altGiftWish = g.AltGiftWish,
                editNotes = g.EditNotes,
                location = g.Location,
                recipientNum = g.RecipientNum,
                labelNum = g.LabelNum,
                name = g.Name,
                status = g.Status
            });
            ;
            return Json(new { gifts = gifts });

        }

        public JsonResult GetAllRecipientsForAgency(int agencyId)
        {
            var rcpts = _recipientViewRepository.GetRecipientsForAgency(agencyId).OrderByDescending(g => g.RecipientNum);

            var recipients = rcpts.Select(r => new
            {
                age = r.Age,
                ageType = r.AgeType,
                gender = r.Gender,
                recipientInfo = r.RecipientInfo,
                giftWish = r.GiftWish,
                altGiftWish = r.AltGiftWish,
                editNotes = r.EditNotes,
                recipientNum = r.RecipientNum,
                name = r.Name,
                status = r.Status
            });
            return Json(new { result = recipients });
        }

        private async Task<TblRecipientParent> GetRecipient(GiftRecipientModel giftRecipientModel)
        {
            var user = await _userManager.GetUserAsync(User);
            var tblRecipientParent = new TblRecipientParent
            {
                Name = giftRecipientModel.Name,
                Age = giftRecipientModel.Age,
                AgeType = giftRecipientModel.AgeType,
                Gender = giftRecipientModel.Gender,
                RecipientInfo = giftRecipientModel.RecipientInfo,
                GiftWish = giftRecipientModel.GiftWish,
                GiftType = giftRecipientModel.GiftType,
                GiftDetail1 = giftRecipientModel.GiftDetail1,
                GiftDetail2 = giftRecipientModel.GiftDetail2,
                AltGiftWish = giftRecipientModel.AltGiftWish,
                AltGiftType = giftRecipientModel.AltGiftType,
                AltGiftDetail1 = giftRecipientModel.AltGiftDetail1,
                AltGiftDetail2 = giftRecipientModel.AltGiftDetail2,
                AgencyId = user.AgencyId,
                Location = giftRecipientModel.Location,
                DateEntered = BusinessMethods.GetLocalDateTime(DateTime.UtcNow),
            };
            return tblRecipientParent;
        }


        private TblStatusLog GenerateStatusLog(int labelNum, int statusId)
        {
            var currentStatus = _context.TblStatusLogs.Where(l => l.LabelNum == labelNum)
                .OrderByDescending(l => l.LogId).FirstOrDefault();
            var currentStatusText = String.Empty;
            if (currentStatus != null)
            {
                currentStatusText = BusinessMethods.GetStatusTypeText(currentStatus.StatusId);
            }

            var presentStatusText = BusinessMethods.GetStatusTypeText(statusId);
            var appId = _context.TblApps.FirstOrDefault(a => a.AppName == "AgencyWeb").AppId;
            var status = new TblStatusLog
            {
                LabelNum = labelNum,
                StatusId = statusId,
                DateEdited = BusinessMethods.GetLocalDateTime(DateTime.UtcNow),
                AppId = appId,
                ChangeInfo = $"Status Changed from {currentStatusText} to {presentStatusText}"

            };
            return status;
        }

        public async Task<ActionResult> ThankYou(int? id)
        {
            int agencyId = 0;
            if (id != null)
            {
                agencyId = id.Value;
                ViewBag.Back = "True";
            }
            else
            {
                var user = await _userManager.GetUserAsync(User);
                agencyId = user.AgencyId.Value;
                ViewBag.Back = "False";
            }

            var agency = _context.TblAgencies.FirstOrDefault(a => a.AgencyId == agencyId);
            ViewBag.AgencyName = agency.AgencyName;
            var model = _thankYouRepository.GetThankYouForAgency(agencyId);

            return View(model);
        }

        public ActionResult AddThankYou(int id)
        {
            var model = new DonorThankYouModel
            {
                RecipientNum = id
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddThankYou(DonorThankYouModel model)
        {
            var donor = GetDonor(model.RecipientNum);
            if (model.FormFiles != null)
            {
                var file = model.FormFiles.First();
                var name = _storageService.AddImageToAzureStorage(file, "thankyous", 550, 550);
                var thankYou = new TblDonorThankYou
                {
                    RecipientNum = model.RecipientNum,
                    Image = name
                };

                thankYou.DonorId = donor.DonorId;

                thankYou.ThankYouDate = BusinessMethods.GetLocalDateTime(DateTime.UtcNow);
                thankYou.Approved = false;
                try
                {
                    _context.TblDonorThankYous.Add(thankYou);
                }
                catch (Exception ex)
                {
                    string input = JsonConvert.SerializeObject(model);
                    var customException = new Exception(input, ex);
                    _logger.LogException(customException);
                    throw;
                }
            }


            if (!String.IsNullOrEmpty(model.Message))
            {
                var thankYou = new TblDonorThankYou
                {
                    Message = model.Message,
                    RecipientNum = model.RecipientNum
                };

                thankYou.DonorId = donor.DonorId;

                thankYou.ThankYouDate = BusinessMethods.GetLocalDateTime(DateTime.UtcNow);
                thankYou.Approved = false;
                _context.TblDonorThankYous.Add(thankYou);
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
            }

            return RedirectToAction("ThankYou");

        }

        private TblDonor GetDonor(int recipientNum)
        {
            var recipient = _context.TblRecipientParents.FirstOrDefault(r => r.RecipientNum == recipientNum);
            if (recipient == null)
            {
                return null;
            }
            var donor = _context.TblDonors.FirstOrDefault(d => d.DonorId == recipient.DonorId);
            return donor;
        }
    }
}
