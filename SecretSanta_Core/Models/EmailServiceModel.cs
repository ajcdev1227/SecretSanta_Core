namespace SecretSanta_Core.Models
{
    public class EmailServiceModel
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public string To { get; set; }
        public string AttachmentContent { get; set; }
        public string AttachmentContentType { get; set; }
        public string AttachmentFileName { get; set; }
        public bool HasAttachment { get; set; }
        public string CC { get; set; }
    }
}
