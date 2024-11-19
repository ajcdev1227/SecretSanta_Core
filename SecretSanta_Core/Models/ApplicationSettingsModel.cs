using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SecretSanta_Core.Data;
using SecretSanta_Core.Models;

namespace SecretSanta_Core.Models
{
    public class ApplicationSettingsModel
    {
        public ApplicationSettingsModel()
        {
            AdminSettings= new List<SettingModel>();
            AdminEmails = new List<TblAdminEmail>();
        }

        public List<SettingModel> AdminSettings { get; set; }
        public Dates HeartCentralDates { get; set; }
        public Dates SecretSantaLetterDates { get; set; }
        public List<TblAdminEmail> AdminEmails { get; set; }
        public List<ImageSettingModel> HomeImageModel { get; set; }
    }

}