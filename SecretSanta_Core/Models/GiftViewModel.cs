using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecretSanta_Core.Models
{
    public class GiftViewModel
    {
        [Display(Name = "Label Num")]
        public int LabelNum { get; set; }
        public int RecipientNum { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string AgeType { get; set; }
        public string Gender { get; set; }
        [Display(Name = "Info")]
        public string RecipientInfo { get; set; }
        [Display(Name = "First Wish")]
        public string GiftWish { get; set; }
        [Display(Name = "Second Wish")]
        public string AltGiftWish { get; set; }
        [Display(Name = "Category")]
        public string Status { get; set; }
        public string EditNotes { get; set; }
        public string Location { get; set; }
        public bool CanEdit { get; set; }
        public string AgeDisplay { get; set; }
    }
}