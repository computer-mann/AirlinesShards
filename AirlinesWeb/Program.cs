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
            services.AddAppDbContexts(builder.Configuration);
            services.AddResponseCaching();
            services.AddRedisOMServices(builder.Configuration);
            services.AddRouting(op => op.LowercaseUrls = true);
           
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