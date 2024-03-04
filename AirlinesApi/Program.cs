using AirlinesApi.Extensions;
using Serilog;
using AirlinesApi.Filters;
using AirlinesApi.WorkerServices;
using Serilog.Events;
using AirlinesApi.Options;
using AirlinesApi.Middlewares;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using AirlinesApi.ViewModels;
using AirlinesApi.ModelValidators;
using FluentValidation.AspNetCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Npgsql;



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
                ConfigureServices(services, builder.Configuration);
                services.AddHostedService<PopulateTravellerTableBackgroundService>();
               
                services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
                services.AddOutputCache();
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen();
               services.AddExceptionHandler<GlobalExceptionHandler>();
                services.AddProblemDetails();
                services.AddAuthenticationCore();
                services.AddAuthorizationCore();
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

        private static void ConfigureServices(IServiceCollection services,IConfiguration configuration)
        {
            
            services.AddControllers();
            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.SuppressModelStateInvalidFilter = true;
            //});
            services.AddScoped<IValidator<LoginViewModel>, LoginValidator>();
            services.AddFluentValidationAutoValidation(options =>
            {
                options.DisableDataAnnotationsValidation = true;
            });
            
        }

        public void Configure(WebApplication app)
        {
            
        }
    }
}