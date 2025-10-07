
using AirlinesApi.Database.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace AirlinesApi.WorkerServices
{
    public class WarmDatabaseHostedService : IHostedService
    {
        private readonly ILogger<PopulateTravellerTableBackgroundService> logger;
        private readonly IServiceProvider provider;

        public WarmDatabaseHostedService(IServiceProvider provider, ILogger<PopulateTravellerTableBackgroundService> logger)
        {
            this.provider = provider;
            this.logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var service = provider.CreateAsyncScope())
            {
                var airlines = service.ServiceProvider.GetRequiredService<AirlinesDbContext>();
                var warm =await airlines.Database.SqlQuery<int>($"select 1").ToListAsync();
                logger.LogInformation("Database Warm");
                await Task.CompletedTask;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
