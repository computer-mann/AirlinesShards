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
            builder.Services.AddOptions<MailSettings>().BindConfiguration("MailSettings").ValidateDataAnnotations().ValidateOnStart();
            builder.Services.AddTransient<IEmailSender, PaperCutLocalEmailSender>();
            var host = builder.Build();
            host.Run();
        }
    }
}