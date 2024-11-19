using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecretSanta_Core.Models
{
    public class DonorModel
    {
        public int RecipientNum { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Display(Name = "Confirm Email")]
        [Compare("Email", ErrorMessage = "The email addresses do not match.")]
        public string ConfirmEmail { get; set; }
    }
}