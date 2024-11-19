using Microsoft.AspNetCore.Mvc;
using SecretSanta_Core.Data;
using SecretSanta_Core.Models;
using SecretSanta_Core.Repositories;
using SecretSanta_Core.Services;

namespace SecretSanta_Core.Controllers
{
	public class AdminThankYouController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ApproveThankYouRepository _approveThankYouRepository;
		private readonly EmailService _emailService;

		public AdminThankYouController(ApplicationDbContext context, ApproveThankYouRepository approveThankYouRepository,
			EmailService emailService)
		{
			_context = context;
			_approveThankYouRepository = approveThankYouRepository;
			_emailService = emailService;
		}

		public ActionResult ApproveThankYous()
		{
			return View();
		}

		public JsonResult GetThankYous()
		{
			var result = _approveThankYouRepository.GetThankYous().OrderByDescending(t => t.DonorThankYouId);
			return Json(new { result });
		}

		public JsonResult GetImageName(int donorThankYouId)
		{
			var thankYou = _context.TblDonorThankYous.FirstOrDefault(t => t.DonorThankYouId == donorThankYouId);
			var imageName = thankYou.Image;
			return Json(new { imageName });
		}

		public JsonResult GetMessage(int donorThankYouId)
		{
			var thankYou = _context.TblDonorThankYous.FirstOrDefault(t => t.DonorThankYouId == donorThankYouId);
			var message = thankYou.Message;
			return Json(new { message });
		}

		public JsonResult SaveMessage(int donorThankYouId, string message)
		{
			var thankYou = _context.TblDonorThankYous.FirstOrDefault(t => t.DonorThankYouId == donorThankYouId);
			thankYou.Message = message;
			_context.TblDonorThankYous.Update(thankYou);
			_context.SaveChanges();
			return Json(new { message = String.Empty });

		}

		public JsonResult ChangeApprove(int donorThankYouId, bool check)
		{
			var thankYou = _context.TblDonorThankYous.FirstOrDefault(t => t.DonorThankYouId == donorThankYouId);
			thankYou.Approved = check;
			_context.TblDonorThankYous.Update(thankYou);
			_context.SaveChanges();
			if (check)
			{
				SendEmail(thankYou).GetAwaiter();
			}
			return Json(new { message = String.Empty });
		}

		private async Task SendEmail(TblDonorThankYou thankYou)
		{
			var donor = _context.TblDonors.FirstOrDefault(d => d.DonorId == thankYou.DonorId);
			var heartUrl = Url.Action("ViewHearts", "MyHeart", new { id = donor.DonorId }, protocol: Request.Scheme);

			var adminEmail = _context.TblAdminEmails.FirstOrDefault(l => l.EmailName == "ThankYouReceived");
			var body = String.Format(adminEmail.EmailContent, new object[]
			{
				heartUrl
			});


			var emailServiceModel = new EmailServiceModel
			{
				To = donor.DonorEmail,
				Subject = "Thank You from your Gift Recipient",
				Message = body,
				HasAttachment = false,
				CC = String.Empty
			};

			await _emailService.SendEmail(emailServiceModel);
		}

	}
}
