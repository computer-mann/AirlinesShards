using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace AirlinesApi.Middlewares
{
    public static class InstrumentationMiddleware
    {
        //https://www.youtube.com/watch?v=CdcApjTBLEM
        public static void AddOpenTelemetryServices(this IServiceCollection services)
        {
            const string serviceName = "nunoo-airlines-api";
            services.AddLogging(logging =>
            {
                logging.AddOpenTelemetry(o =>
                {
                    o.AddOtlpExporter().SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName));
                });
            });
            services.AddOpenTelemetry()
                .ConfigureResource(resource =>
                {

                    resource.AddService(serviceName);
                })
                .WithTracing(tracing =>
                {
                    tracing.AddNpgsql();
                    tracing.AddAspNetCoreInstrumentation();
                    //tracing.AddHttpClientInstrumentation();
                    tracing.AddEntityFrameworkCoreInstrumentation();
                    tracing.AddRedisInstrumentation();
                    tracing.AddOtlpExporter();
                });
            //.WithMetrics(metrics =>
            // {
            //     metrics.AddAspNetCoreInstrumentation()
            //     .AddMeter("Microsoft.AspNetCore.Hosting")
            //     .AddMeter("Microsoft.AspNetCore.Server.Kestrel");
            //     metrics.AddConsoleExporter();
            // })
        }
    }
}
