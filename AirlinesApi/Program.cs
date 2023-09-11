using Infrastructure.Extensions;
using Serilog;
using Shared_Presentation.Filters;
using Infrastructure.WorkerServices;
using Serilog.Events;

namespace AirlinesApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] [{SourceContext}] {Message}{NewLine}{Exception}")
                .CreateBootstrapLogger();
            try
            {
                builder.Host.UseSerilog();
                var services = builder.Services;
                // Add services to the container.
                //services.AddHostedService<PopulateTrouperTableBackgroundService>();
                services.AddControllers(options =>
                {
                    options.Filters.Add<LogRequestTimeAndDurationActionFilter>();
                });
                services.AddOutputCache();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen();
               
                services.AddRedisOMServices(builder.Configuration);
                services.AddAppDbContexts(builder.Configuration);
                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();
                app.UseOutputCache();
                app.UseAuthorization();


                app.MapControllers();
                app.Logger.LogInformation("App starting at {0}", DateTime.Now);
                app.Run();
            }catch(Exception ex)
            {
                string type = ex.GetType().Name;
                if (!type.Equals("StopTheHostException", StringComparison.Ordinal))
                {
                    Log.Fatal(ex, "Something serious happened, Failed to start.");
                }
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }
    }
}