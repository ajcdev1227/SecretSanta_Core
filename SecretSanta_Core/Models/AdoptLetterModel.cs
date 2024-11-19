using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecretSanta_Core.Models
{
    public class AdoptLetterModel
    {
        public int LetterId { get; set; }
        public string Letter { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter your name")]
        public string AdoptedBy { get; set; }
        [RegularExpression(@"^[2-9]\d{2}[-][2-9]\d{2}[-]\d{4}$", ErrorMessage = "Enter valid phone in format 999-999-9999")]
        [Display(Name = "Phone")]
        public string AdoptedByPhone { get; set; }
        [EmailAddress(ErrorMessage = "Not a valid email address")]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        public string AdoptedByEmail { get; set; }
        [Display(Name = "Confirm Email")]
        [Compare("AdoptedByEmail", ErrorMessage = "The email addresses do not match.")]
        public string ConfirmEmail { get; set; }
        public string LetterSummary { get; set; }
        public List<FamilyMemberModel> FamilyMembers { get; set; }
        public string GiftMethod { get; set; }
    }
}