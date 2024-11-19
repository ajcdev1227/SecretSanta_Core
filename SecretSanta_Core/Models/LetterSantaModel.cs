using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SecretSanta_Core.Models;

namespace secretsanta.Models
{
    public class LetterSantaModel
    {
        public WriterInfoModel? WriterModel { get; set; }
        public string? Agency { get; set; }
        public FamilyInfoModel? FamilyInfo { get; set; }
        public LetterModel? Letter { get; set; }
    }
}