namespace EmailSenderWorkerService.Services
{
    internal class MailKitEmailSender : IEmailSender
    {
        public Task<bool> Send(string subject, string body, string recipientEmail, string customerName)
        {
            throw new NotImplementedException();
        }
    }
}
