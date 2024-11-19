using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecretSanta_Core.Data;
using SecretSanta_Core.Models;
using SecretSanta_Core.Repositories;
using System.Data;
using System.Transactions;
using SecretSanta_Core.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace SecretSanta_Core.Controllers
{
	public class AgencyUserController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly AgencyUserRepository _repository;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly EmailService _emailService;

		public AgencyUserController(
			ApplicationDbContext context, 
			AgencyUserRepository repository, 
			UserManager<ApplicationUser> userManager,
			EmailService emailService)
		{
			_context = context;
			_repository = repository;
			_userManager = userManager;
			_emailService = emailService;
		}

		[Authorize(Roles = "Admin")]
		public ActionResult Index()
		{
			var agencies = _context.TblAgencies.ToList().Where(a => a.IsActive);
			return View(agencies.OrderBy(a => a.AgencyName));
		}

		[Authorize(Roles = "Primary, Leader")]
		public async Task<ActionResult> AgencyContacts()
		{
			var user = await _userManager.GetUserAsync(User);

			var model = new List<AgencyContactModel>();
			if (user.AgencyId.HasValue)
			{
				var temp = _repository.GetContactsForAgency(user.AgencyId.Value);

				var first = temp.First();
				ViewBag.AgencyName = first.AgencyName;
				ViewBag.AgencyId = first.AgencyId;
				var list = temp.Where(u => u.Id != user.Id);
				if (list.Any())
				{
					var userRoles = _repository.UserRoles();
					foreach (var u in list)
					{
						var roles = userRoles.Where(usr => usr.UserId == u.Id).Select(r => r.RoleName);
						u.Roles = String.Join(", ", roles.ToArray());
					}

					model = list.OrderBy(c => c.Id).ToList();
				}
			}

			return View(model);
		}

		[Authorize(Roles = "Admin")]
		public IActionResult AllAgencyContacts()
		{
			var list = _repository.GetAllAgencyContacts();
			var userRoles = _repository.UserRoles();
			foreach (var u in list)
			{
				var roles = userRoles.Where(usr => usr.UserId == u.Id).Select(r => r.RoleName);
				u.Roles = String.Join(", ", roles.ToArray());
			}

			var model = list.OrderBy(a => a.AgencyName).ThenBy(c => c.Id).ToList();
			return View(model);

		}

		[Authorize(Roles = "Admin, Primary, Leader")]
		public async Task<ActionResult> Delete(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			
			using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
			try
			{
				user.Archive = true;
				await _userManager.UpdateAsync(user);
				scope.Complete();
			}
			catch
			{
				scope.Dispose();
				throw;
			}

			if (User.IsInRole("Admin"))
			{
				return RedirectToAction("AllAgencyContacts");
			}

			return RedirectToAction("AgencyContacts");
		}

		[HttpPost]
		[Authorize(Roles = "Admin, Primary, Leader")]
		public async Task<ActionResult> Edit(AgencyContactModel agencyContactModel)
		{
			if(agencyContactModel.Roles == "Leader")
			{
				int leaders = _repository.checkLeaders((int)agencyContactModel.AgencyId);
				if (leaders > 0)
				{
					string leaderId = _repository.getLeader((int)agencyContactModel.AgencyId);
					if(leaderId != agencyContactModel.Id)
					{
						var leader = await _userManager.FindByIdAsync(leaderId);
						var rolesForLeader = await _userManager.GetRolesAsync(leader);
						if (rolesForLeader.Any())
						{
							foreach (var item in rolesForLeader.ToList())
							{
								await _userManager.RemoveFromRoleAsync(leader, item);
							}
						}
						await _userManager.AddToRoleAsync(leader, "Primary");
					}
				}
			}

			var tblAgencyContact = _context.TblAgencyContacts.FirstOrDefault(e => e.Id == agencyContactModel.Id);
			var user = await _userManager.FindByIdAsync(agencyContactModel.Id);
			using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
			try
			{
				if(agencyContactModel.Roles != null)
				{
					var rolesForUser = await _userManager.GetRolesAsync(user);
					if (rolesForUser.Any())
					{
						foreach (var item in rolesForUser.ToList())
						{
							await _userManager.RemoveFromRoleAsync(user, item);
						}
					}
					await _userManager.AddToRoleAsync(user, agencyContactModel.Roles);
				}
				if(agencyContactModel.Archive != null)
					user.Archive = agencyContactModel.Archive;
				if (agencyContactModel.IsActive != null)
					user.IsActive = agencyContactModel.IsActive;
				if (tblAgencyContact != null)
				{
					tblAgencyContact.FirstName = agencyContactModel.FirstName;
					tblAgencyContact.LastName = agencyContactModel.LastName;
					tblAgencyContact.Phone = agencyContactModel.Phone;
					tblAgencyContact.AltPhone = agencyContactModel.AltPhone;
					tblAgencyContact.Fax = agencyContactModel.Fax;
					tblAgencyContact.EstimateWishes = agencyContactModel.EstimateWishes;
				}
				_context.SaveChanges();
				scope.Complete();
			}
			catch
			{
				scope.Dispose();
				throw;
			}

			if (User.IsInRole("Admin"))
			{
				return RedirectToAction("AllAgencyContacts");
			}

			return RedirectToAction("AgencyContacts");
		}

		[Authorize(Roles = "Primary, Leader")]
		public ActionResult AddAgencyPickup(int id)
		{
			var model = new AgencyPickUpModel
			{
				AgencyId = id
			};
			return View(model);
		}

		[Authorize(Roles = "Admin, Primary, Leader")]
		public async Task<ActionResult> ResetPassword(string id)
		{
			var user = await _userManager.FindByIdAsync(id);

			using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
			try
			{
				var code = await _userManager.GeneratePasswordResetTokenAsync(user);
				code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

				await SendLoginEmail(user.Email, code);
				scope.Complete();
			}
			catch
			{
				scope.Dispose();
				throw;
			}

			if (User.IsInRole("Admin"))
			{
				return RedirectToAction("AllAgencyContacts");
			}

			return RedirectToAction("AgencyContacts");
		}

		private async Task SendLoginEmail(string email, string code)
		{

			var callbackUrl = Url.Page(
				"/Account/ResetPassword",
				pageHandler: null,
				values: new { area = "Identity", code },
				protocol: Request.Scheme);
			var adminEmail = _context.TblAdminEmails.FirstOrDefault(l => l.EmailName == "Login Email");
			var content = String.Format(adminEmail.EmailContent, new object[]
			{
				email, callbackUrl
			});


			var emailServiceModel = new EmailServiceModel
			{
				To = email,
				Subject = "Secret Santa Login Info",
				Message = content,
				HasAttachment = false,
				CC = String.Empty
			};

			await _emailService.SendEmail(emailServiceModel);

		}

		[Authorize(Roles = "Admin")]
		public JsonResult checkLeaders(int agencyId)
		{
			return Json(new { count = _repository.checkLeaders(agencyId) });
		}
	}
}
