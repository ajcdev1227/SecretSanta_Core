namespace SecretSanta_Core
{
    public class AppSettings
    {
        public string? EmailFrom { get; set; }

        public string? EmailTo { get; set; }

        public string? ContactEmail { get; set; }

        public string? AdminEmail { get; set; }

        public string? VolunteerCenter { get; set; }

        public string? SupportEmail { get; set; }

        public string? USPSUser { get; set; }

        public string? StripeAPIKey { get; set; }

        public string? StripeSecretKey { get; set; }

        public string? StorageUriBase { get; set; }

        public int HeartsPageSize { get; set; }

        public string? StorageConnectionString { get; set; }
    }
}
