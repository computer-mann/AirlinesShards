using EmailSenderWorkerService.Model;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace EmailSenderWorkerService.Services
{
    public class PaperCutLocalEmailSender : IEmailSender
    {
        private readonly MailSettings _mailSettings;
        private readonly ILogger<PaperCutLocalEmailSender> _logger;

        public PaperCutLocalEmailSender(IOptions<MailSettings> mailSettings, ILogger<PaperCutLocalEmailSender> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }
        public async Task<bool> Send(string subject, string body, string recipientEmail,string customerName)
        {
            //try
            //{
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(_mailSettings.SenderEmail, _mailSettings.SenderName);
                message.To.Add(new MailAddress(recipientEmail));
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;
                smtp.Port = _mailSettings.Port;
                smtp.Host = _mailSettings.Server;
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_mailSettings.SenderName, "_mailSettings.Password");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                await smtp.SendMailAsync(message);

                return true;
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Email Sending to Papercut Failed");
            //    // Exception Details
            //    return false;
            //}
        }
    }
}
