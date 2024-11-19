namespace SecretSanta_Core.Models
{
    public class ThankYouRecipientModel
    {
        public int RecipientNum { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string AgeType { get; set; }
        public string Gender { get; set; }
        public string RecipientInfo { get; set; }
        public string GiftWish { get; set; }
        public string AltGiftWish { get; set; }
    }
}
