using EmailSenderWorkerService.Services;
using MassTransit;

namespace EmailSenderWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IBus _bus;

        public Worker(ILogger<Worker> logger,IEmailSender email,IBus bus)
        {
            _bus = bus;
            _logger = logger;
            this._emailSender = email;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    if (await _emailSender.Send("Test Email", "Verification Code", "hpsn16@gmail.com", "Prince User"))
                    {
                        _logger.LogInformation("Email Sent Successfully");
                    }
                    else
                    {
                        _logger.LogError("Email Sending Failed");
                    }
                }
                await Task.Delay(1500, stoppingToken);
                
            }
        }
    }
}
