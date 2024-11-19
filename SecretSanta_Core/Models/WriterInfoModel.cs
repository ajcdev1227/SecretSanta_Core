using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SecretSanta_Core.Models
{
	public class WriterInfoModel
	{
		[Display(Name = "Name")]
		[Required(ErrorMessage = "Required")]
		public string WriterName { get; set; }

		[Required(ErrorMessage = "Required")]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Required")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Required")]
		[Display(Name = "Address")]
		public string Address { get; set; }

		[Required(ErrorMessage = "Required")]
		[Display(Name = "City")]
		public string City { get; set; }

		[Required(ErrorMessage = "Required")]
		[Display(Name = "Zip")]
		public string Zip { get; set; }

		[Required(ErrorMessage = "Required")]
		[Display(Name = "Phone")]
		public string Phone { get; set; }

		[Display(Name = "Email")]
		[Required(ErrorMessage = "Required")]
		[EmailAddress(ErrorMessage = "InvalidEmail")]
		public string Email { get; set; }

        [Display(Name = "Agency")]
		[Required(ErrorMessage = "Required")]
		public string Agency { get; set; }
    }
}