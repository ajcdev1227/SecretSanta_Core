using System.Diagnostics;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SecretSanta_Core.BusinessLogic;
using SecretSanta_Core.Data;
using SecretSanta_Core.Models;
using SecretSanta_Core.Repositories;
using SecretSanta_Core.Services;
using Stripe;

namespace SecretSanta_Core.Controllers
{
	public class AdoptHeartController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly RankSubNumberService _rankSubNumberService;
		private readonly AdoptHeartRepository _adoptHeartRepository;
		private readonly AppSettings _appSettings;
		private readonly ExceptionUtilityService _exceptionUtility;
		private readonly EmailService _emailService;


		public AdoptHeartController(ApplicationDbContext context, RankSubNumberService rankSubNumberService, AdoptHeartRepository adoptHeartRepository, IOptions<AppSettings> appSettings,
			ExceptionUtilityService exceptionUtility, EmailService emailService)
		{
			_context = context;
			_rankSubNumberService = rankSubNumberService;
			_adoptHeartRepository = adoptHeartRepository;
			_appSettings = appSettings.Value;
			_exceptionUtility = exceptionUtility;
			_emailService = emailService;
		}

		public List<AdoptAHeartModel> OrderedHearts
		{
			get
			{
				//if (HttpContext.Session.GetObject<List<AdoptAHeartModel>>("OrderHearts") == null)
				{
					var orderedHearts = _adoptHeartRepository.GetHearts().OrderByDescending(h => h.WebRank)
						.ThenBy(m => m, new Comparer(_rankSubNumberService));
					HttpContext.Session.SetObject("OrderHearts", orderedHearts.ToList());
				}

				return HttpContext.Session.GetObject<List<AdoptAHeartModel>>("OrderHearts");
			}
		}

		public IActionResult AdoptFromHome(int? id)
		{
			var page = id != null ? id.Value : 1;
			ViewBag.GiftTypes = _context.GiftDetails.OrderBy(g => g.GiftDetailOrder)
				.Select(g => new { Id = g.GiftDetailId, Text = g.GiftDetailText })
				.ToDictionary(x => x.Id, x => x.Text);
			var model = GetModel(page);
			ViewBag.Count = OrderedHearts.Count;
			ViewBag.PageSize = Convert.ToInt32(_appSettings.HeartsPageSize);
			ViewBag.PageNumber = page;
			HttpContext.Session.SetInt32("Page", page);
			ViewBag.GiftDueDate = _context.ApplicationSettings.FirstOrDefault(s => s.SettingsName == "Gift Due Date").SettingsValue;
			return View(model);
		}

		private List<AdoptAHeartModel> GetModel(int page)
		{
			var model = OrderedHearts;
			if (HttpContext.Session.GetString("Gender") != null && HttpContext.Session.GetString("Gender") != "")
			{
				var gender = HttpContext.Session.GetString("Gender");
				model = model.Where(m => m.Gender == gender).ToList();
			}

			if (HttpContext.Session.GetInt32("AgeGroup") != null && HttpContext.Session.GetInt32("AgeGroup") != 0)
			{
				var group = HttpContext.Session.GetInt32("AgeGroup");
				switch (group)
				{
					case 1:
						model = model.Where(m => m.Age >= 0 && m.Age <= 12).ToList();
						break;
					case 2:
						model = model.Where(m => m.Age >= 13 && m.Age <= 24).ToList();
						break;
					case 3:
						model = model.Where(m => m.Age >= 25 && m.Age <= 36).ToList();
						break;
					case 4:
						model = model.Where(m => m.Age >= 37 && m.Age <= 64).ToList();
						break;
					case 5:
						model = model.Where(m => m.Age >= 65).ToList();
						break;
				}
			}

			if (HttpContext.Session.GetInt32("GiftType") != null && HttpContext.Session.GetInt32("GiftType") != 0)
			{
				var giftType = HttpContext.Session.GetInt32("GiftType");
				model = model.Where(m => m.GiftType == giftType).ToList();
			}


			int pageSize = _appSettings.HeartsPageSize;
			int skip = (page - 1) * pageSize;
			var hearts = model.Skip(skip).Take(pageSize).ToList();
			foreach (var m in hearts)
			{
				m.NameAgeGenderString = HeartMethods.GetNameAgeGenderString(m);
				m.FirstWishString = HeartMethods.GetWishString(m.FirstWish, m.GiftDetail1, m.GiftDetail2);
				m.SecondWishString = HeartMethods.GetWishString(m.SecondWish, m.AltGiftDetail1, m.AltGiftDetail2);
				m.YearString = m.DateEntered.Year.ToString();
			}


			ViewBag.PageNumber = page;

			return hearts;
		}


		public JsonResult FilterHearts()
		{
			var model = GetModel(1);

			return Json(new { results = model });
		}

		public JsonResult FilterOnGender(string gender)
		{
			HttpContext.Session.SetInt32("Page", 1);
			if (gender != null)
				HttpContext.Session.SetString("Gender", gender);
			else
				HttpContext.Session.SetString("Gender", "");
			return FilterHearts();
		}

		public JsonResult FilterOnAge(int ageGroup)
		{
			HttpContext.Session.SetInt32("Page", 1);
			HttpContext.Session.SetInt32("AgeGroup", ageGroup);
			return FilterHearts();
		}

		public JsonResult FilterOnGiftType(int giftType)
		{

			HttpContext.Session.SetInt32("Page", 1);
			HttpContext.Session.SetInt32("GiftType", giftType);
			return FilterHearts();
		}

		public JsonResult Clear()
		{
			HttpContext.Session.Remove("Gender");
			HttpContext.Session.Remove("AgeGroup");
			HttpContext.Session.Remove("GiftType");
			HttpContext.Session.SetInt32("Page", 1);
			return FilterHearts();
		}

		public JsonResult Page(int id)
		{
			HttpContext.Session.SetInt32("Page", id);
			var model = GetModel(id);
			return Json(new { results = model });
		}

		public JsonResult GetInitialFilterValues()
		{
			var gender = HttpContext.Session.GetString("Gender") != null ? HttpContext.Session.GetString("Gender") : "";
			var ageGroup = HttpContext.Session.GetInt32("AgeGroup") != null ? HttpContext.Session.GetInt32("AgeGroup").ToString() : "";
			var giftType = HttpContext.Session.GetInt32("GiftType") != null ? HttpContext.Session.GetInt32("GiftType").ToString() : "";
			return Json(new { gender, ageGroup, giftType });
		}

		public ActionResult AdoptHeart(int? id)
		{
			AdoptAHeartModel? model = null;
			if (id.HasValue)
			{
				try
				{
					model = PopulateHeartModel(id.Value);
					HttpContext.Session.SetObject("Heart", model);
				}
				catch (Exception ex)
				{
					_exceptionUtility.LogException(ex);
					throw;
				}
			}

			ViewBag.Page = HttpContext.Session.GetInt32("Page");

			return View(model);
		}

		[HttpPost]
		public ActionResult AdoptHeart(AdoptAHeartModel model)
		{
			if (model.GiftMethod == "Gift")
			{
				var donor = ProcessAdoption(model, 0);
				var heartModel = HttpContext.Session.GetObject<AdoptAHeartModel>("Heart");
				SendGiftEmail(model.Donor.Email, donor.DonorId, heartModel).GetAwaiter();
				return RedirectToAction("PrintHeart");
			}

			HttpContext.Session.SetObject("HeartModel", model);
			return RedirectToAction("Payment");
		}

		public JsonResult CheckDonor(int recipientNum)
		{
			var recipient = _context.TblRecipientParents.FirstOrDefault(r => r.RecipientNum == recipientNum);
			var count = 0;
			if (recipient != null && !String.IsNullOrEmpty(recipient.DonorId))
			{
				count = 1;
			}
			return Json(new { count });
		}

		public IActionResult Payment()
		{
			var model = new PaymentModel()
			{
				StripePublishableKey = _appSettings.StripeAPIKey,
			};
			ViewBag.Amount = "50.00";
			return View(model);
		}

		[HttpPost]
		public ActionResult Payment(PaymentModel model)
		{
			var heartModel = HttpContext.Session.GetObject<AdoptAHeartModel>("Heart");
			var email = HttpContext.Session.GetObject<AdoptAHeartModel>("HeartModel").Donor.Email;

			var chargeService = new ChargeService();
			var chargeOptions = new ChargeCreateOptions()
			{
				Amount = (int)(model.Amount * 100),
				Currency = "USD",
				Description = string.Format($"Secret Santa Donation for Recipient Num {heartModel.RecipientNumber}")
			};

			if (email == null)
			{
				chargeOptions.Source = model.StripeToken;
			}
			else
			{
				var customers = new CustomerService();
				var customer = customers.Create(new CustomerCreateOptions
				{
					Email = email,
					Source = model.StripeToken
				});
				chargeOptions.Customer = customer.Id;
				chargeOptions.ReceiptEmail = email;
			}

			try
			{
				var charge = chargeService.Create(chargeOptions);
				var donor = ProcessAdoption(HttpContext.Session.GetObject<AdoptAHeartModel>("HeartModel"), model.Amount);
				SendDonationEmail(donor, model.Amount, heartModel).GetAwaiter();
				return RedirectToAction("PaymentConfirmation");
			}
			catch (StripeException stripeException)
			{
				Debug.WriteLine(stripeException.Message);
				ModelState.AddModelError(string.Empty, stripeException.Message);
				return View(model);
			}
		}

		public ActionResult PaymentConfirmation()
		{
			return View();
		}

		private TblDonor ProcessAdoption(AdoptAHeartModel model, decimal amount)
		{
			string donorId;
			var donor = _context.TblDonors.FirstOrDefault(d => d.DonorEmail == model.Donor.Email);
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
					DonorEmail = model.Donor.Email
				};

				_context.TblDonors.Add(tblDonor);

				_context.SaveChanges();

				donor = tblDonor;
			}

			using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
			try
			{
				var recipient = _context.TblRecipientParents.FirstOrDefault(p => p.RecipientNum == model.RecipientNumber);
				recipient.DonorId = donorId;
				recipient.DonorRegisterDate = BusinessMethods.GetLocalDateTime(DateTime.UtcNow);
				_context.TblRecipientParents.Update(recipient);

				if (model.GiftMethod == "Donate")
				{
					var donation = new TblDonation
					{
						RecipientNum = model.RecipientNumber,
						Amount = amount,
						DonDateTime = BusinessMethods.GetLocalDateTime(DateTime.UtcNow)
					};
					_context.TblDonations.Add(donation);
				}

				var label = _context.TblRecipientChildren.FirstOrDefault(l => l.Primary && l.RecipientNum == model.RecipientNumber);

				var statusReg = GenerateStatusLog(
						label.LabelNum,
						model.GiftMethod == "Donate" ? (int)Enumerations.StatusTypes.FundAHeart : (int)Enumerations.StatusTypes.HomeAdopt
					);
				_context.TblStatusLogs.Add(statusReg);
				_context.SaveChanges();
				var heart = OrderedHearts.FirstOrDefault(h => h.RecipientNumber == model.RecipientNumber);
				OrderedHearts.Remove(heart);
				scope.Complete();
			}
			catch (Exception ex)
			{
				_exceptionUtility.LogException(ex);
				scope.Dispose();
				throw;
			}

			return donor;
		}

		public ActionResult PrintHeart()
		{
			var model = HttpContext.Session.GetObject<AdoptAHeartModel>("Heart");
			ViewBag.EmailTo = _appSettings.ContactEmail;
			ViewBag.GiftDueDate = _context.ApplicationSettings.FirstOrDefault(s => s.SettingsName == "Gift Due Date").SettingsValue;
			return View(model);
		}

		private AdoptAHeartModel PopulateHeartModel(int id)
		{
			var model = _adoptHeartRepository.GetHeart(id);
			if (model != null)
			{

				model.NameAgeGenderString = HeartMethods.GetNameAgeGenderString(model);
				model.FirstWishString =
					HeartMethods.GetWishString(model.FirstWish, model.GiftDetail1, model.GiftDetail2);
				model.SecondWishString =
					HeartMethods.GetWishString(model.SecondWish, model.AltGiftDetail1, model.AltGiftDetail2);
			}
			return model;
		}

		private TblStatusLog GenerateStatusLog(int labelNum, int statusId)
		{
			var currentStatusId = _context.TblStatusLogs
											.OrderByDescending(l => l.LogId)
											.FirstOrDefault(l => l.LabelNum == labelNum);
			var currentStatusText = BusinessMethods.GetStatusTypeText(currentStatusId.StatusId);
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

		private async Task SendGiftEmail(string emailTo, string donorId, AdoptAHeartModel heartModel)
		{
			var adminEmail = _context.TblAdminEmails.FirstOrDefault(l => l.EmailName == "Register Heart");
			var body = String.Format(adminEmail.EmailContent, new object[]
			{
				donorId,
				heartModel.Name,
				heartModel.Age,
				heartModel.Gender,
				heartModel.FirstWishString,
				heartModel.SecondWishString,
				heartModel.RecipientNumber,
				heartModel.LabelNum
			});


			var emailServiceModel = new EmailServiceModel
			{
				To = emailTo,
				Subject = "My Heart Registration",
				Message = body,
				HasAttachment = false,
				CC = _appSettings.AdminEmail
			};

			await _emailService.SendEmail(emailServiceModel);

		}

		private async Task SendDonationEmail(TblDonor donor, decimal amount, AdoptAHeartModel heartModel)
		{
			var adminEmail = _context.TblAdminEmails.FirstOrDefault(l => l.EmailName == "Donation Email");
			var body = String.Format(adminEmail.EmailContent, new object[]
			{
				donor.DonorEmail,
				amount,
				donor.DonorId,
				heartModel.Name,
				heartModel.Age,
				heartModel.Gender,
				heartModel.FirstWishString,
				heartModel.SecondWishString,
				heartModel.RecipientNumber,
				heartModel.LabelNum
			});

			var emailServiceModel = new EmailServiceModel
			{
				To = donor.DonorEmail,
				Subject = "Donation Received",
				Message = body,
				HasAttachment = false,
				CC = _appSettings.AdminEmail
			};

			await _emailService.SendEmail(emailServiceModel);
		}

		internal class Comparer : IComparer<AdoptAHeartModel>
		{
			private readonly RankSubNumberService _rankSubNumberService;

			internal Comparer(RankSubNumberService rankSubNumberService)
			{
				_rankSubNumberService = rankSubNumberService;
			}

			public int Compare(AdoptAHeartModel? x, AdoptAHeartModel? y)
			{
				if (_rankSubNumberService.GetRankSubNumber(x.RecipientNumber) > _rankSubNumberService.GetRankSubNumber(y.RecipientNumber))
					return 1;
				if (_rankSubNumberService.GetRankSubNumber(x.RecipientNumber) < _rankSubNumberService.GetRankSubNumber(y.RecipientNumber))
					return -1;
				return 0;

			}
		}
	}
}
