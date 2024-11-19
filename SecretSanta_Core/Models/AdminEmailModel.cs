using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecretSanta_Core.Models
{
    public class AdminEmailModel
    {
        public int EmailId { get; set; }
        public string EmailName { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Email content is required")]
        [Display(Name = "Email Content")]
        public string EmailContent { get; set; }
    }
}