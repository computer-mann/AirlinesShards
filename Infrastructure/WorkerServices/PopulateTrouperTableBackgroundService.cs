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
            var database = connectionMultiplexer.GetDatabase();
            using (var service = provider.CreateAsyncScope())
            {
                var userManager = service.ServiceProvider.GetRequiredService<UserManager<Trouper>>();
                var trouperContext = service.ServiceProvider.GetRequiredService<TrouperDbContext>();
                var airlinesContext = service.ServiceProvider.GetRequiredService<AirlinesDbContext>();
                while (!stoppingToken.IsCancellationRequested)
                {
                    /*
                    if (!await database.KeyExistsAsync(distinctKey))
                    {
                        int customers =await airlinesContext.Tickets.Select(e => e.PassengerName).Distinct().CountAsync();
                        int ticketTableRecords =await airlinesContext.Tickets.CountAsync();
                        if (await database.StringSetAsync(distinctKey, customers) && await database.StringSetAsync(ticketTableRecordsNumKey, ticketTableRecords))
                        {
                            logger.LogInformation("customer number set");
                        }
                        else
                        {
                            logger.LogError("something went wrong");
                        }
                    }
                    else
                    {
                        int count = int.Parse((await database.StringGetAsync(ticketTableRecordsNumKey))!);
                        logger.LogInformation("key exists");
                        
                        while (numberProcessed < count)
                        {
                            GC.Collect();
                            var tickets =await airlinesContext.Tickets.OrderBy(e => e.TicketNo).Skip(numberProcessed).Take(take).ToListAsync();
                            foreach(var user in tickets)
                            {
                                var contactData=JsonSerializer.Deserialize<ContactData>(user.ContactData!);
                                if (string.IsNullOrEmpty(contactData!.Email) ||
                                    string.IsNullOrEmpty(contactData.Phone) ||
                                    await database.SetContainsAsync(passengerNameSetkey, user.PassengerName))
                                {
                                   // if(DateTime.Now.Second % 7 == 0) logger.LogInformation("contact data has nulls or in set, skipping.");

                                    continue;
                                }

                               var userResult=await userManager.CreateAsync(new Trouper
                                {
                                    PhoneNumber=contactData.Phone,
                                    PhoneNumberConfirmed=true,
                                    PassengerName=user.PassengerName,
                                    Email=contactData.Email ?? contactData.Phone,
                                    EmailConfirmed=true,
                                    Country="Russia"
                                },"password1");
                                if (userResult.Succeeded)
                                {
                                    //if (DateTime.Now.Second % 11 == 0) logger.LogInformation("User was saved to db");
                                    if(await database.SetAddAsync(passengerNameSetkey, user.PassengerName))
                                    {
                                      //  if(DateTime.Now.Second%9 == 0) logger.LogInformation("pasenger name was added to set");

                                    }
                                }
                                else
                                {
                                    logger.LogInformation("Whats wrong->  {0}", userResult.Errors);
                                }
                            }
                            numberProcessed += take;
                            await Task.Delay(2000);
                        }
                    }
                    */
                    
                    logger.LogInformation("Asmongold {0}", DateTime.Now);
                    await Task.Delay(9000);
                }
                logger.LogInformation("service ending.");
            }
        }
    }
}
