using Serilog;
using AirlinesApi.Options;
using AirlinesApi.Middlewares;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using AirlinesApi.Infrastructure;
using AirlinesApi.Services;
using Microsoft.OpenApi;
using Airlines.Common.Middlewares;



namespace AirlinesApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq")!)
                .WriteTo.Async(wt=>wt.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] [{SourceContext}] {Message}{NewLine}{Exception}"))
                .CreateBootstrapLogger();
            try
            {
                builder.Host.UseSerilog();
                var services = builder.Services;
                // Add services to the container.
                ConfigureServices(services, builder.Configuration);
                
                var app = builder.Build();
                Configure(app);
              
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
            var jwt = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            
            services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,options =>
            {
                
                options.TokenValidationParameters =
                  new TokenValidationParameters
                  {
                      ValidAudience = jwt!.Audience,
                      ValidIssuer = jwt.Audience,
                      ValidateIssuer=true,
                      ValidateAudience=true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key!))
                  };
                
            });
            services.AddAuthorization();
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.MaxDepth = 0;
            }); 
            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.SuppressModelStateInvalidFilter = true;
            //});
            services.AddValidatorsFromAssembly(typeof(Program).Assembly);
            services.AddScoped<ICustomCacheService,CustomCacheService>();
           
            //services.AddHostedService<PopulateTravellerTableBackgroundService>();
            //services.AddHostedService<WarmDatabaseHostedService>();

            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            services.AddStackExchangeRedisOutputCache(options =>
            {
                options.Configuration =
                    configuration.GetConnectionString("Redis");
            });
            services.AddOutputCache(options =>
            {
                options.DefaultExpirationTimeSpan = TimeSpan.FromMinutes(10);
                
                options.AddPolicy("IgnoreAuthCache",policy =>
                {
                    policy.Tag("IgnoreAuthCache");
                    policy.AddPolicy<IgnoreAuthorizationOutputCachePolicy>();
                    policy.Expire(TimeSpan.FromMinutes(20));
                },true);
            });
            services.AddEndpointsApiExplorer();
            
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
            services.AddRedisOMServices(configuration);
            services.AddAppDbContexts(configuration);
            services.AddOpenTelemetryServices();

        }

        public static void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            app.UseOutputCache();
            if (app.Environment.IsDevelopment())
            {
                
            }
            app.UseExceptionHandler();
            app.UseHttpsRedirection();
            app.UseAuthorization();


            app.MapControllers();

            app.Logger.LogInformation("App starting at {0}", DateTime.Now);
            app.Run();
        }
    }
}