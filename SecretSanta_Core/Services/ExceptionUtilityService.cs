using SecretSanta_Core.BusinessLogic;
using SecretSanta_Core.Data;
using SecretSanta_Core.Models;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using Azure.Core;
using Microsoft.Extensions.Options;

namespace SecretSanta_Core.Services
{
    public class ExceptionUtilityService
    {
        private readonly EmailService _emailService;
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;

        public ExceptionUtilityService(EmailService emailService, IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _emailService = emailService;
            _appSettings = appSettings.Value;
            _context = context;
        }
        
        public void LogException(Exception ex)
        {
            var secretSantaLog = new SecretSantaLog
            {
                LogDateTime = BusinessMethods.GetLocalDateTime(DateTime.UtcNow),
                ExceptionType = !String.IsNullOrEmpty(ex.GetType().ToString())? ex.GetType().ToString(): !String.IsNullOrEmpty(ex.InnerException.GetType().ToString())? ex.InnerException.GetType().ToString() : String.Empty,
                ExceptionMessage = ex.Message,
                ExceptionSource = !String.IsNullOrEmpty(ex.Source)? ex.Source : !String.IsNullOrEmpty(ex.InnerException.Source)? ex.InnerException.Source : String.Empty,
                StackTrace = !String.IsNullOrEmpty(ex.StackTrace) ? ex.StackTrace : !String.IsNullOrEmpty(ex.InnerException.StackTrace)? ex.InnerException.StackTrace : String.Empty, 
            };
            _context.SecretSantaLogs.Add(secretSantaLog);
            _context.SaveChanges();

            _ = SendExceptionEmail(ex);
        }


        private async Task SendExceptionEmail(Exception ex)
        {
            var body = GetEmailBody(ex);
            var subject = "Secret Santa Error";

            var emailServiceModel = new EmailServiceModel
            {
                To = _appSettings.SupportEmail,
                Subject = subject,
                Message = body,
                HasAttachment = false,
                CC = String.Empty
            };

            await _emailService.SendEmail(emailServiceModel);
        }

        private string GetEmailBody(Exception ex)
        {
            var exceptionType = !String.IsNullOrEmpty(ex.GetType().ToString()) ? ex.GetType().ToString() : !String.IsNullOrEmpty(ex.InnerException.GetType().ToString()) ? ex.InnerException.GetType().ToString() : String.Empty;
            var exceptionSource = !String.IsNullOrEmpty(ex.Source) ? ex.Source : !String.IsNullOrEmpty(ex.InnerException.Source) ? ex.InnerException.Source : String.Empty;
            var stackTrace = !String.IsNullOrEmpty(ex.StackTrace) ? ex.StackTrace : !String.IsNullOrEmpty(ex.InnerException.StackTrace) ? ex.InnerException.StackTrace : String.Empty;
            var content = new StringBuilder();
            content.AppendLine($"<p>LogDateTime: {BusinessMethods.GetLocalDateTime(DateTime.UtcNow)}</p>");
            content.AppendLine($"<p>Exception Type: {exceptionType}</p>");
            content.AppendLine($"<p>Exception Message: {ex.Message}</p>");
            content.AppendLine($"<p>Exception Source: {exceptionSource}</p>");
            content.AppendLine($"<p>StackTrace: {stackTrace}</p>");

            return content.ToString();

        }

    }
}
