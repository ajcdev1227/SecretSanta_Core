using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecretSanta_Core.Models
{
    public class GuideMeModel
    {
        [Required(ErrorMessage="Required")]
        [DataType(DataType.MultilineText)]
        public string Problem { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.MultilineText)]
        public string Need { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.MultilineText)]
        public string Benefit { get; set; }
    }
}