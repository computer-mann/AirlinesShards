using Domain.Tables;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.WorkerServices
{
    public class PopulateTrouperTableBackgroundService : BackgroundService
    {
        private const string distinctKey = "distinctCustomerNo";
        private const string ticketTableRecordsNumKey = "ticketTableRecords";
        private const string passengerNameSetkey = "passengerNameSet";
        private readonly ILogger<PopulateTrouperTableBackgroundService> logger;
        private readonly IServiceProvider provider;
        private readonly IConnectionMultiplexer connectionMultiplexer;

        public PopulateTrouperTableBackgroundService(ILogger<PopulateTrouperTableBackgroundService> logger,IServiceProvider provider,IConnectionMultiplexer connectionMultiplexer)
        {
            this.logger = logger;
            this.provider = provider;
            this.connectionMultiplexer = connectionMultiplexer;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Starting background service:");
            int numberProcessed = 0;
            int take = 100_000;
            int skippedUsers = 0;
            
            var database = connectionMultiplexer.GetDatabase();
            using (var service = provider.CreateAsyncScope())
            {
                var userManager = service.ServiceProvider.GetRequiredService<UserManager<Trouper>>();
                var trouperContext = service.ServiceProvider.GetRequiredService<TrouperDbContext>();
                var airlinesContext = service.ServiceProvider.GetRequiredService<AirlinesDbContext>();
                logger.LogInformation("memory for before db call => {0}mb", CheckGcMemoryInMb());
                var users=userManager.Users.ToList();
                logger.LogInformation("memory for all troupers table => {0}mb", CheckGcMemoryInMb());
                //var tickets = airlinesContext.Tickets.OrderBy(e=>e.TicketNo)
                  //  .Where(e=>!string.IsNullOrEmpty(e.PassengerId)).Take(42).ToList();
                
                logger.LogInformation("about to update the tickets.");
                for (int i= 0; i < users.Count ;i++ )
                {
                    var user = users[i];
                   await airlinesContext.Tickets.Where(e => e.PassengerName == user.PassengerName && e.PassengerId == null)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(b=>b.PassengerId, user.Id));
                    if(i % 1000 == 0)
                    {
                        logger.LogInformation("proces at {0}th", i);
                    }
                }
                 airlinesContext.SaveChanges();
                logger.LogInformation("ended foreach");
                logger.LogInformation("about to iterate through the updated tickets.");
                while (!stoppingToken.IsCancellationRequested)
                {
                    
                    logger.LogInformation("Asmongold {0}", DateTime.Now);
                    await Task.Delay(6000);
                }
                logger.LogInformation("service ending.");
            }
        }

        private long CheckGcMemoryInMb()
        {
            return GC.GetTotalMemory(false) / 1024/1024;
        }
    }
}
