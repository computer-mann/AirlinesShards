using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Redis.OM;
using StackExchange.Redis;

namespace AirlinesWeb
{
    public static class StartUp
    {
        public static void AddRedisOMServices(this IServiceCollection services,IConfiguration configuration)
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
            services.AddDbContext<AirlinesDbContext>(optionsAction);

            services.AddDbContext<TrouperDbContext>(optionsAction);
        }
    }
}
