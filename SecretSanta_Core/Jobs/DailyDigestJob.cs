using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
using Quartz;
using SecretSanta_Core.Controllers;
using SecretSanta_Core.Data;
using SecretSanta_Core.Models;
using SecretSanta_Core.Repositories;
using SecretSanta_Core.Services;
using System.Drawing;


namespace SecretSanta_Core.Jobs
{
	public class DailyDigestJob : IJob
	{
		private readonly ApplicationDbContext _context;
		private readonly EmailService _emailService;
		private readonly AgencyUserRepository _agencyUserRepository;
		private readonly GiftViewRepository _giftViewRepository;
		public DailyDigestJob(
			ApplicationDbContext context, 
			AgencyUserRepository repository,
			GiftViewRepository giftViewRepository,
			EmailService emailService)
		{
			_context = context;
			_emailService = emailService;
			_agencyUserRepository = repository;
			_giftViewRepository = giftViewRepository;
		}
		
		public Task Execute(IJobExecutionContext context)
		{
			var heartCentralSetting = _context.ApplicationSettings.FirstOrDefault(s => s.Category == "HeartCentral");
			var dates = JsonConvert.DeserializeObject<TheseDates>(heartCentralSetting.SettingsValue);
			DateTime currentDate = DateTime.Now;
			if(dates.StartDate <= currentDate && currentDate <= dates.EndDate)
			{
				var list = _agencyUserRepository.GetActiveAgencyContacts();
				foreach(var u in list)
				{
					SendEmail(u);
				}
			}
			
			return Task.FromResult(true);
		}

		private void SendEmail(AgencyContactModel model)
		{
			List<int> webGroup = new List<int>() { 8, 9, 10 };
			var stringHtml = $"<h3> DEAR {model.AgencyName}</h2>";
			stringHtml += "<p>Please review the status of your gifts below. If you have more than 20 gifts awaiting pickup it is urgent to do so to avoid a tripping hazard at Heart Central.</p>";
			for (int i = 0; i <  webGroup.Count; i++)
			{
				List<int> subWebGroup = new List<int> { webGroup[i] };
				var gfts = _giftViewRepository.GetGiftsByStatus(subWebGroup, (int)model.AgencyId).OrderByDescending(g => g.RecipientNum);
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

				stringHtml += $"<p>{GiftViewRepository.DashboardTypeStrings[webGroup[i]]}: {gifts.Count()}</p>";
				stringHtml += "<table style = 'font-family: arial, sans-serif;border-collapse: collapse;width: 100%;'>";
				stringHtml += "<tr>" +
					"<th style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>Location</th>" +
					"<th style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>Recipient Num</th>" +
					"<th style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>Label Num</th>" +
					"<th style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>Name</th>" +
					"<th style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>Age</th>" +
					"<th style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>Gender</th>" +
					"<th style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>Info</th>" +
					"<th style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>First Wish</th>" +
					"<th style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>Second Wish</th>" +
					"<th style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>Status</th>" +
					"<th style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>Edit Notes</th>" +
					"</tr>";

				foreach (var gift in gifts)
				{
					var age = gift.age != null && gift.ageType != null ? gift.age + " " + gift.ageType : "";
					var gender = gift.gender != null ? gift.gender : "";
					var info = gift.recipientInfo != null ? gift.recipientInfo : "";
					var giftWish = gift.giftWish != null ? gift.giftWish : "";
					var altGiftWish = gift.altGiftWish != null ? gift.altGiftWish : "";
					var editNotes = gift.editNotes != null ? gift.editNotes : "";
					var location = gift.location != null ? gift.location : "";
					stringHtml += "<tr>";
					stringHtml += "<td style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>" + location + "</td>";
					stringHtml += "<td style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>" + gift.recipientNum + "</td>";
					stringHtml += "<td style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>" + gift.labelNum + "</td>";
					stringHtml += "<td style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>" + gift.name + "</td>";
					stringHtml += "<td style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>" + age + "</td>";
					stringHtml += "<td style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>" + gender + "</td>";
					stringHtml += "<td style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>" + info + "</td>";
					stringHtml += "<td style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>" + giftWish + "</td>";
					stringHtml += "<td style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>" + altGiftWish + "</td>";
					stringHtml += "<td style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>" + gift.status + "</td>";
					stringHtml += "<td style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>" + editNotes + "</td>";
					stringHtml += "</tr>";
				}

				stringHtml += "</table>";
			}
			
			var emailServiceModel = new EmailServiceModel
			{
				To = model.Email,
				Subject = "Daily Digest",
				Message = stringHtml,
				HasAttachment = false,
				CC = String.Empty
			};

			_emailService.SendEmail(emailServiceModel);
		}
	}
}
