using Infrastructure.Extensions;
using Serilog;
using AirlinesApi.Filters;
using Infrastructure.WorkerServices;
using Serilog.Events;
using Infrastructure.Options;
using AirlinesApi.Middlewares;



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
                //services.AddHostedService<PopulateTravellerTableBackgroundService>();
                services.AddControllers(options =>
                {
                    options.Filters.Add<LogRequestTimeAndDurationActionFilter>();
                });
                services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
                services.AddOutputCache();
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen();
               services.AddExceptionHandler<GlobalExceptionHandler>();
                services.AddProblemDetails();
                services.AddRedisOMServices(builder.Configuration);
                services.AddAppDbContexts(builder.Configuration);
                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                app.UseExceptionHandler();
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