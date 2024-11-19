using Microsoft.AspNetCore.Identity;

namespace SecretSanta_Core.Data
{
	public class ApplicationUser : IdentityUser
	{
		public bool IsConfirmed { get; set; }

		public int? AgencyId { get; set; }

		public bool Archive { get; set; }

		public bool IsActive { get; set; }

	}
}
