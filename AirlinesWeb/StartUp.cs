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
    }
}
