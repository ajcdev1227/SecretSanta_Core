using Newtonsoft.Json;
using SecretSanta_Core.Models;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace SecretSanta_Core.Services
{
    public class EmailService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EmailService(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public async Task SendEmail(EmailServiceModel email)
        {
            var url = "https://prod-92.westus.logic.azure.com:443/workflows/e3d31d8a904c4c6d9c803a4d971b2eb8/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=Enl9Tk75-IMHm2iUs5D5Rvrw9GtyVk-hm3X_ZEV89U0";
            string jsonData;

            if (email.HasAttachment)
            {
                jsonData = JsonConvert.SerializeObject(new
                {
                    to = email.To,
                    subject = email.Subject,
                    message = email.Message,
                    cc = email.CC,
                    attachmentContent = email.AttachmentContent,
                    attachmentContentType = email.AttachmentContentType,
                    attachmentFileName = email.AttachmentFileName

                });
            }
            else
            {
                jsonData = JsonConvert.SerializeObject(new
                {
                    to = email.To,
                    subject = email.Subject,
                    message = email.Message,
                    cc = email.CC,
                });
            }

            var httpClient = _httpClientFactory.CreateClient();
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await httpClient.PostAsync(url, content);
        }
    }
}
