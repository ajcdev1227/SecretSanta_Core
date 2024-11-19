using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SecretSanta_Core.BusinessLogic;
using SecretSanta_Core.Data;
using SecretSanta_Core.Models;
using SecretSanta_Core.Repositories;
using SecretSanta_Core.Services;

namespace SecretSanta_Core.Controllers
{
	public class MyHeartController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly AdoptHeartRepository _repository;
		private readonly EmailService _emailService;
		private readonly AppSettings _appSettings;
		private readonly ExceptionUtilityService _logger;

		public MyHeartController(ApplicationDbContext context, AdoptHeartRepository repository, EmailService emailService, IOptions<AppSettings> appSettings, ExceptionUtilityService logger)
		{
			_context = context;
			_repository = repository;
			_emailService = emailService;
			_appSettings = appSettings.Value;
			_logger = logger;
		}

		public ActionResult Index()
		{
			return View();
		}

		public JsonResult GetLabel(int heartNumber)
		{
			TblRecipientParent recipient;
			var label = _context.TblRecipientChildren
				.Where(l => l.LabelNum == heartNumber || l.RecipientNum == heartNumber)
				.FirstOrDefault();

			if (label == null)
			{
				return Json(new { status = "fail", message = "No label could be found" });
			}

			var registered = _context.TblStatusLogs
			  .OrderBy(l => l.StatusId)
			  .Where(l => l.LabelNum == label.LabelNum)
			  .LastOrDefault();

			if (registered != null && registered.StatusId >= 59)
			{
				return Json(new { status = "fail", message = "This heart has already been registered" });
			}

			var heartData = _repository.GetHeart(label.RecipientNum);
			var heart = new
			{
				name = heartData.Name,
				labelNum = heartData.LabelNum,
				agencyCode = heartData.AgencyCode,
				recipientNumber = heartData.RecipientNumber
			};
			var year = heartData.DateEntered.Year;
			return Json(new { status = "success", heart, year });
		}

		[HttpPost]
		public ActionResult Index(DonorModel model)
		{
			string donorId;
			var donor = _context.TblDonors.FirstOrDefault(d => d.DonorEmail == model.Email);
			if (donor != null)
			{
				donorId = donor.DonorId;
			}
			else
			{
				donorId = Guid.NewGuid().ToString();
				var tblDonor = new TblDonor
				{
					DonorId = donorId,
					DonorEmail = model.Email
				};
				_context.TblDonors.Add(tblDonor);

				_context.SaveChanges();
			}

			var recipient = _context.TblRecipientParents.Include(p => p.TblRecipientChildren).FirstOrDefault(r => r.RecipientNum == model.RecipientNum);
			recipient.DonorId = donorId;
			recipient.DonorRegisterDate = BusinessMethods.GetLocalDateTime(DateTime.UtcNow);
			_context.TblRecipientParents.Update(recipient);
			var labelNum = recipient.TblRecipientChildren.FirstOrDefault(c => c.Primary).LabelNum;
			var status = GenerateStatusLog(labelNum, (int)Enumerations.StatusTypes.HeartRegistered);
			_context.TblStatusLogs.Add(status);
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

			SendEmail(model.Email, donorId, recipient, labelNum).GetAwaiter();

			return RedirectToAction("ViewHearts", new { id = donorId });

		}

		public ActionResult ViewHearts(string id)
		{
			var model = new List<DonorThankYousModel>();
			var hearts = _repository.GetHeartsForDonor(id).OrderByDescending(h => h.RecipientNumber);
			foreach (var h in hearts)
			{
				var mdl = new DonorThankYousModel();
				var m = new AdoptAHeartModel
				{
					DateEntered = h.DateEntered,
					RecipientNumber = h.RecipientNumber,
					RecipientInfo = h.RecipientInfo,
					NameAgeGenderString = HeartMethods.GetNameAgeGenderString(h),
					FirstWishString = HeartMethods.GetWishString(h.FirstWish, h.GiftDetail1, h.GiftDetail2),
					SecondWishString = HeartMethods.GetWishString(h.SecondWish, h.AltGiftDetail1, h.AltGiftDetail2),
				};
				mdl.Heart = m;
				var thankYous = _context.TblDonorThankYous.Where(t => t.DonorId == id && t.RecipientNum == h.RecipientNumber && t.Approved);
				var thankyousModel = thankYous.Select(t => new DonorThankYouModel
				{
					DonorId = t.DonorId,
					Message = t.Message,
					Image = t.Image
				});
				mdl.MessageThankYous = thankyousModel.Where(t => !String.IsNullOrEmpty(t.Message)).ToList();
				mdl.ImageThankYous = thankyousModel.Where(t => !String.IsNullOrEmpty(t.Image)).ToList();
				model.Add(mdl);
			}

			ViewBag.ThankyouImageUriBase = _appSettings.StorageUriBase + "thankyous";
			return View(model);
		}

		private TblStatusLog GenerateStatusLog(int labelNum, int statusId)
		{
			var currentStatusId = _context.TblStatusLogs.Where(l => l.LabelNum == labelNum)
				.OrderByDescending(l => l.LogId).First().StatusId;
			var currentStatusText = BusinessMethods.GetStatusTypeText(currentStatusId);
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

		private async Task SendEmail(string emailTo, string donorId, TblRecipientParent tblRecipientParent, int labelNum)
		{
			var adminEmail = _context.TblAdminEmails.FirstOrDefault(l => l.EmailName == "Register Heart");
			var body = String.Format(adminEmail.EmailContent, new object[]
			{
				donorId,
				tblRecipientParent.Name,
				tblRecipientParent.Age,
				tblRecipientParent.Gender,
				HeartMethods.GetWishString(tblRecipientParent.GiftWish, tblRecipientParent.GiftDetail1, tblRecipientParent.GiftDetail2),
				HeartMethods.GetWishString(tblRecipientParent.AltGiftWish, tblRecipientParent.AltGiftDetail1, tblRecipientParent.AltGiftDetail2),
				tblRecipientParent.RecipientNum,
				labelNum
			});

			var emailServiceModel = new EmailServiceModel
			{
				To = emailTo,
				Subject = "My Heart Registration",
				Message = body,
				HasAttachment = false,
				CC = String.Empty
			};

			await _emailService.SendEmail(emailServiceModel);

		}
	}
}
