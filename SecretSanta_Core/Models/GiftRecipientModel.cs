using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SecretSanta_Core.Models
{
    public class GiftRecipientModel
    {
        [Display(Name = "Recipient Num")]
        public int RecipientNum { get; set; }

        [StringLength(25)]
        public string Name { get; set; }

        [RegularExpression(@"^\d+$")]
        public int? Age { get; set; }

        public string AgeType { get; set; }

        public string Gender { get; set; }

        [Display(Name = "Gift Recipient Personal Information")]
        [StringLength(35)]
        public string RecipientInfo { get; set; }

        [Display(Name = "First Wish")]
        [StringLength(25)]
        public string GiftWish { get; set; }

        [Display(Name = "Category")]
        public int? GiftType { get; set; }

        [StringLength(15)]
        public string GiftDetail1 { get; set; }

        [StringLength(15)]
        public string GiftDetail2 { get; set; }

        [Display(Name = "Second Wish")]
        [StringLength(25)]
        public string AltGiftWish { get; set; }

        [Display(Name = "Category")]
        public int? AltGiftType { get; set; }

        [StringLength(15)]
        public string AltGiftDetail1 { get; set; }

        [StringLength(15)]
        public string AltGiftDetail2 { get; set; }

        public string? Location { get; set; }

        public string Status { get; set; }

        public string EditNotes { get; set; }

        public string AgeDisplay { get; set; }

        public int AgencyID { get; set; }
    }
}