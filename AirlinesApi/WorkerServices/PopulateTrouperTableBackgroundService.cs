using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;
using AirlinesApi.Database.Models;
using Redis.OM;
using AirlinesApi.Infrastructure;
using Redis.OM.Searching;
using System;


namespace AirlinesApi.WorkerServices
{
    public class PopulateTravellerTableBackgroundService : BackgroundService
    {
        private readonly RedisConnectionProvider _redisProvider;
        private readonly ILogger<PopulateTravellerTableBackgroundService> logger;
        private readonly IServiceProvider provider;

        public PopulateTravellerTableBackgroundService(ILogger<PopulateTravellerTableBackgroundService> logger, IServiceProvider provider,
             RedisConnectionProvider redisProvider)
        {
            this.logger = logger;
            this.provider = provider;
            _redisProvider = redisProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Starting background service:");

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    logger.LogInformation("Asmongold {0}", DateTime.Now);
            //    await Task.Delay(6000);
            //}
           // await PopulateRedisWithUserTable();
            logger.LogInformation("service ending.");
        }
        private async Task PopulateRedisWithUserTable()
        {
            using (var service = provider.CreateAsyncScope())
            {
                var userManager = service.ServiceProvider.GetRequiredService<UserManager<Traveller>>();
                var users = userManager.Users.ToList();
                List<RedisTraveller> redisTravellers = new();
                foreach (var user in users)
                {
                    redisTravellers.Add(RedisTraveller.ToRedisTraveller(user));
                }
               await _redisProvider.Connection.CreateIndexAsync(typeof(RedisTraveller));
                RedisCollection<RedisTraveller> _people=(RedisCollection<RedisTraveller>)_redisProvider.RedisCollection<RedisTraveller>();
                await _people.InsertAsync(redisTravellers);
                await Task.CompletedTask;
            }
        }

    }
}
