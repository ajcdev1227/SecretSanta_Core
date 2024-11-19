using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecretSanta_Core.Models
{
    public class LetterModel
    {
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.MultilineText)]
        public string LetterText { get; set; }
    }
}