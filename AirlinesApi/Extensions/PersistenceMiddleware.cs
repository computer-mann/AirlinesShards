using AirlinesApi;
using Domain.Tables;
using Domain.Validators;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Redis.OM;
using StackExchange.Redis;

namespace Infrastructure.Extensions
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
            Action<DbContextOptionsBuilder>? optionsAction = (options) => options.UseNpgsql(configuration.GetConnectionString("Database"));
            //services.AddDbContext<AirlinesDbContext>(optionsAction);
            services.Replace(ServiceDescriptor.Scoped<IUserValidator<Traveller>, CustomTravellerValidator>());
            services.AddDbContext<TravellerDbContext>(optionsAction);
            //services.AddDbContext<CompanyDbContext>(optionsAction);

            services.AddIdentityCore<Traveller>()
                .AddEntityFrameworkStores<TravellerDbContext>()
                .AddUserValidator<CustomTravellerValidator>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                
            });
            
        }
    }
}
