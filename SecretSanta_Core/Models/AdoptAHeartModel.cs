using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SecretSanta_Core.Models
{
    public class AdoptAHeartModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        public string AgeType { get; set; }

        public string Gender { get; set; }

        public int GiftType { get; set; }

        public int AltGiftType { get; set; }

        public string FirstWish { get; set; }

        public string SecondWish { get; set; }

        public string GiftDetail1 { get; set; }

        public string GiftDetail2 { get; set; }

        public string AltGiftDetail1 { get; set; }

        public string AltGiftDetail2 { get; set; }

        [JsonProperty("recipientInfo")]
        public string RecipientInfo { get; set; }

        [JsonProperty("recipientNumber")]
        public int RecipientNumber { get; set; }

        [JsonProperty("agencyCode")]
        public string AgencyCode { get; set; }

        [JsonProperty("labelNum")]
        public int LabelNum { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("nameAgeGenderString")]
        public string NameAgeGenderString { get; set; }

        [JsonProperty("firstWishString")]
        public string FirstWishString { get; set; }

        [JsonProperty("secondWishString")]
        public string SecondWishString { get; set; }

        public DateTime DateEntered { get; set; }

        [JsonProperty("yearString")]
        public string YearString { get; set; }

        public string Status { get; set; }

        public DonorModel Donor { get; set; }

        public string GiftMethod { get; set; }

        public int WebRank { get; set; }
    }
}