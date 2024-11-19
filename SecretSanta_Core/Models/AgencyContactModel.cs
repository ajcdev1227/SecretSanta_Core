using System.ComponentModel.DataAnnotations;

namespace SecretSanta_Core.Models
{
    public class AgencyContactModel
    {
        public string Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        public string Phone { get; set; }
       
        [Display(Name = "Alt Phone")]
        public string AltPhone { get; set; }
        
        public string Fax { get; set; }

        [Display(Name = "Estimate Wishes")]
        public int? EstimateWishes { get; set; }

		[Display(Name = "Archive")]
		public bool Archive { get; set; }

		[Display(Name = "Status")]
        public bool IsActive { get; set; }

        public string Roles { get; set; }

        public string Email { get; set; }

        [Display(Name = "Agency Name")]
        public string AgencyName { get; set; }

        public int? AgencyId { get; set; }
    }
}
