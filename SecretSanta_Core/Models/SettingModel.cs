using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecretSanta_Core.Models
{
    public class SettingModel
    {
        public int SettingsID { get; set; }
        [Display(Name = "Setting Name")]
        [Required(ErrorMessage = "Setting Name is required")]
        public string SettingsName { get; set; }
        [Display(Name = "Setting Value")]
        [Required(ErrorMessage = "Setting Value is required")]
        public string SettingsValue { get; set; }
        public string Category { get; set; }
    }
}