using Newtonsoft.Json;

namespace SecretSanta_Core.Models
{
    public class ApproveThankYouModel
    {
        [JsonProperty("donorThankYouId")]
        public int DonorThankYouId { get; set; }
        [JsonProperty("recipientName")]
        public string RecipientName { get; set; }
        [JsonProperty("thankYouDate")]
        public string ThankYouDate { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("approved")]
        public bool Approved { get; set; }
    }
}
