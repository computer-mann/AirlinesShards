namespace BookingProcessorWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        CancellationTokenSource _cts=new CancellationTokenSource(TimeSpan.FromSeconds(5));

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!_cts.IsCancellationRequested)
                {
                    
                        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                        
                    await Task.Delay(1000, stoppingToken);
                }
                _cts.Token.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(ex, "Task was cancelled");
                _logger.LogInformation("Worker with cts stopped at: {time}", DateTimeOffset.Now);
                throw new Exception("something went wrong");
            }finally
            {
                _cts.Dispose();
                //Environment.Exit(1);
            }
            
        }
    }
}
