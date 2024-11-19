using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecretSanta_Core.Models
{
    public class RecipientViewModel
    {

        public int RecipientNum { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string AgeType { get; set; }
        public string Gender { get; set; }
        public string RecipientInfo { get; set; }
        public string GiftWish { get; set; }
        public string AltGiftWish { get; set; }
        public string Status { get; set; }
        public string EditNotes { get; set; }
        public bool CanEdit { get; set; }
        public string AgeDisplay { get; set; }
    }
}