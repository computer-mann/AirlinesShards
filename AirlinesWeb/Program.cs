using AirlinesWeb.Models.DbContexts;
using Microsoft.EntityFrameworkCore;
using Redis.OM;
using StackExchange.Redis;

namespace AirlinesWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services=builder.Services;
            // Add services to the container.
           services.AddControllersWithViews();
            services.AddDbContext<AirlinesContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
            });
            services.AddResponseCaching();
            // services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")));
            var mux = ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis"), options =>
            {
                options.DefaultDatabase = 0;
            });
            services.AddSingleton(new RedisConnectionProvider(mux));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseResponseCaching();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}