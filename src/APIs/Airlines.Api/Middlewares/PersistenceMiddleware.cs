using Domain.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Redis.OM;
using StackExchange.Redis;
using AirlinesApi.Database.DbContexts;
using AirlinesApi.Database.Models;
using MassTransit;

namespace AirlinesApi.Middlewares
{
    public static class PersistenceMiddleware
    {
        public static void AddRedisOMServices(this IServiceCollection services, IConfiguration configuration)
        {
            var mux = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!, options =>
            {
                options.DefaultDatabase = 0;
            });
            services.AddSingleton<IConnectionMultiplexer>(mux);
            services.AddSingleton(new RedisConnectionProvider(mux));
        }
        public static void AddAppDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            Action<DbContextOptionsBuilder>? optionsAction = (options) =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Database"));
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            };
            services.AddDbContext<AirlinesDbContext>(optionsAction);
            services.Replace(ServiceDescriptor.Scoped<IUserValidator<Traveller>, CustomTravellerValidator>());

            services.AddIdentityCore<Traveller>()
                .AddEntityFrameworkStores<TravellerDbContext>()
                .AddDefaultTokenProviders()
                .AddUserValidator<CustomTravellerValidator>();

            services.AddDbContext<CompanyDbContext>(optionsAction);
            services.AddSingleton<DapperAirlinesContext>();
            services.AddDbContext<TravellerDbContext>(optionsAction);

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

            });

        }
        public static void AddMassTransitServices(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("cacctuccjacc");
                        h.Password("Klop9090");
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    } 
}
