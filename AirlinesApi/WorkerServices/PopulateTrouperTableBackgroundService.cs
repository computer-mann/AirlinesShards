using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;
using AirlinesApi.Database.Models;
using Redis.OM;
using AirlinesApi.Infrastructure;
using Redis.OM.Searching;
using System;
using AirlinesApi.Database.DbContexts;
using Microsoft.EntityFrameworkCore;


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
          //  await AddUserNametoUserstable();
            logger.LogInformation("service ending.");
        }
        private async Task PopulateRedisWithUserTable()
        {
            //https://redis.io/docs/interact/search-and-query/basic-constructs/configuration-parameters/
            using (var service = provider.CreateAsyncScope())
            {
                var userManager = service.ServiceProvider.GetRequiredService<UserManager<Traveller>>();
                var users = userManager.Users.ToList();
                
                RedisCollection<RedisTraveller> _people = (RedisCollection<RedisTraveller>)_redisProvider.RedisCollection<RedisTraveller>();
                 await _redisProvider.Connection.CreateIndexAsync(typeof(RedisTraveller));
                
                int numInCache = 0;
                while(numInCache < users.Count)
                {
                    List<RedisTraveller> redisTravellers = new();
                    foreach (var user in users.Skip(numInCache).Take(100)) 
                    {
                         redisTravellers.Add(RedisTraveller.ToRedisTraveller(user));
                    }
                    await _people.InsertAsync(redisTravellers);
                    numInCache += 100;
                }
                await Task.CompletedTask;
            }
        }

        private async Task AddUserNametoUserstable()
        {
            using (var service = provider.CreateAsyncScope())
            {
                var userManager = service.ServiceProvider.GetRequiredService<UserManager<Traveller>>();
                var airlinesDb = service.ServiceProvider.GetRequiredService<AirlinesDbContext>();
                var users = userManager.Users.ToList();
                var newUsers = new List<Traveller>();
                SortedSet<string> usernameSet = new(); //username -> userid
                string newUsername = "";
                foreach(var user in users)
                {
                    var firstname = user.PassengerName.Split(' ')[0];
                    var lastname=user.PassengerName.Split(' ')[1];
                    newUsername= firstname+"_"+lastname.Substring(0,2);
                    
                    if (!usernameSet.Add(newUsername))
                    {
                        newUsername=lastname+"."+firstname.Substring(0,2);
                        usernameSet.Add(newUsername);
                        user.UserName=newUsername;
                        user.SecurityStamp = "a";
                        user.NormalizedUserName= newUsername.ToUpper();
                        newUsers.Add(new Traveller()
                        {
                            Id= user.Id,
                            NormalizedUserName= newUsername.ToUpper(),
                            AccessFailedCount=user.AccessFailedCount,
                            ConcurrencyStamp=user.ConcurrencyStamp,
                            Country=user.Country,
                            Email=user.Email,
                            EmailConfirmed=user.EmailConfirmed,
                            LockoutEnabled=user.LockoutEnabled,
                            LockoutEnd=user.LockoutEnd,
                            NormalizedEmail=user.NormalizedEmail,
                            PassengerName=user.PassengerName,
                            PasswordHash=user.PasswordHash,
                            PhoneNumber=user.PhoneNumber,
                            PhoneNumberConfirmed=user.PhoneNumberConfirmed,
                            SecurityStamp=user.SecurityStamp,
                            TwoFactorEnabled=user.TwoFactorEnabled,
                            UserName= newUsername
                        });
                        // airlinesDb.Travellers.ExecuteUpdate(t=>t.SetProperty(v=>v.UserName, newUsername));
                    }
                    else
                    {
                        newUsers.Add(new Traveller()
                        {
                            Id = user.Id,
                            NormalizedUserName = newUsername.ToUpper(),
                            AccessFailedCount = user.AccessFailedCount,
                            ConcurrencyStamp = user.ConcurrencyStamp,
                            Country = user.Country,
                            Email = user.Email,
                            EmailConfirmed = user.EmailConfirmed,
                            LockoutEnabled = user.LockoutEnabled,
                            LockoutEnd = user.LockoutEnd,
                            NormalizedEmail = user.NormalizedEmail,
                            PassengerName = user.PassengerName,
                            PasswordHash = user.PasswordHash,
                            PhoneNumber = user.PhoneNumber,
                            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                            SecurityStamp = user.SecurityStamp,
                            TwoFactorEnabled = user.TwoFactorEnabled,
                            UserName = newUsername
                        });
                    }
                    
                }
                airlinesDb.Travellers.UpdateRange(newUsers);
               await airlinesDb.SaveChangesAsync();
            }
             await Task.CompletedTask;
        }
    }
}
