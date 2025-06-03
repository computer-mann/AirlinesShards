using EmailSenderWorkerService.Services;
using MassTransit;
using Polly;
using Polly.Retry;
using System.Net.Mail;

namespace EmailSenderWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEmailSender _emailSender;
        //private readonly IBus _bus;

        public Worker(ILogger<Worker> logger,IEmailSender email)
        {
            //_bus = bus;
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
                    ResiliencePipeline<Boolean> pipeline = new ResiliencePipelineBuilder<Boolean>().AddRetry(new RetryStrategyOptions<Boolean>()
                    {
                        BackoffType = DelayBackoffType.Constant,
                        Delay = TimeSpan.FromSeconds(7),
                        MaxRetryAttempts = 5,
                        ShouldHandle = new PredicateBuilder<Boolean>().Handle<SmtpException>(),
                        OnRetry = retryArguments =>
                        {
                            _logger.LogError(retryArguments.Outcome.Exception, "Error from polly sending email retry count: {retryCount}", retryArguments.AttemptNumber);
                            return ValueTask.CompletedTask;
                        }
                    }).AddTimeout(TimeSpan.FromSeconds(10))
                      .Build();
                    var emailSentResult = await pipeline.ExecuteAsync(async result =>  await _emailSender.Send("Test Email", "This is a test email", "hpsn16@gmail.com", "Test User"));
                   if (emailSentResult)
                   {
                       _logger.LogInformation("Email sent successfully");
                    }
                    else
                    {

                      _logger.LogError("Email sending failed");
                    }
                   
                }
                await Task.Delay(2500, stoppingToken);
                
            }
        }
    }
}
