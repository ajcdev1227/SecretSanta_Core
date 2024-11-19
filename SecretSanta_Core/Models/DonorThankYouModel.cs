using System.ComponentModel.DataAnnotations;

namespace SecretSanta_Core.Models
{
    public class DonorThankYouModel
    {
        public string? Message { get; set; }
        public int RecipientNum { get; set; }
        public string DonorId { get; set; }
        public DateTime ThankYouDate { get; set; }
        public IFormFile[]? FormFiles { get; set; }
        public string Image { get; set; }
    }
}
