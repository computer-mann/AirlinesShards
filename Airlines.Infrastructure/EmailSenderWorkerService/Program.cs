using EmailSenderWorkerService.Model;
using EmailSenderWorkerService.Services;

namespace EmailSenderWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            var host = builder.Build();
            host.Run();
        }
    }
}