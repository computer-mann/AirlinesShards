using Infrastructure.Extensions;
using Shared_Presentation.Filters;

namespace AirlinesWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services=builder.Services;
            // Add services to the container.
            services.AddControllersWithViews(configure =>
            {
                configure.Filters.Add<LogRequestTimeAndDurationActionFilter>();
            });
            services.AddAppDbContexts(builder.Configuration);
            services.AddResponseCaching();
            services.AddRedisOMServices(builder.Configuration);
            services.AddRouting(op => op.LowercaseUrls = true);
            services.AddOutputCache();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error/500");
                
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpLogging();
            app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseResponseCaching();
            app.UseRouting();
            app.UseOutputCache();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{statuscode?}");
            app.Logger.LogInformation("App starting {0}", DateTime.Now);
            app.Run();

            
        }
    }
}