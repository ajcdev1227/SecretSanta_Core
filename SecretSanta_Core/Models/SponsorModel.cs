using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecretSanta_Core.Models
{
    public class SponsorModel
    {
        [Display(Name="Sponsor")]
        public string SponsorName { get; set; }
        public string Location { get; set; }
        public string GoogleUrl { get; set; }
    }
}