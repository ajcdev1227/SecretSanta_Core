using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecretSanta_Core.Models
{
	public class FamilyInfoModel
	{
		[Display(Name = "NumChildrenUnder19")]
		public int? NumChildrenUnder19 { get; set; }

		[Display(Name = "NumChildrenOver19")]
		public int? NumChildrenOver19 { get; set; }

		[Display(Name = "NumParents")]
		public int? NumParents { get; set; }

		[Display(Name = "NumGrandparents")]
		public int? NumGrandparents { get; set; }

		[Display(Name = "NumOtherFamily")]
		public int? NumOtherFamily { get; set; }
	}
}