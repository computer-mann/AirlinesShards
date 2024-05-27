namespace EmailSenderWorkerService.Services
{
    public interface IEmailSender
    {
        Task<bool> Send(string subject, string body, string recipientEmail, string customerName);
    }
}