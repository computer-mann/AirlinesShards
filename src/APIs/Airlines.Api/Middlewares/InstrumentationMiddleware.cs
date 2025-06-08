using AirlinesApi.Telemetry;
using Npgsql;
using OpenTelemetry;
using OpenTelemetry.Context.Propagation;
using OpenTelemetry.Extensions.Propagators;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace AirlinesApi.Middlewares
{
    public static class InstrumentationMiddleware
    {
        //https://www.youtube.com/watch?v=CdcApjTBLEM
        public static void AddOpenTelemetryServices(this IServiceCollection services)
        {
            services.AddOpenTelemetry()
                .ConfigureResource(resource =>
                {
                    resource.AddService(TelemetryConstants.ServiceName);
                })
                .WithTracing(tracing =>
                {
                    Sdk.SetDefaultTextMapPropagator(new CompositeTextMapPropagator(new TextMapPropagator[]
                    {
                        new TraceContextPropagator(),
                        new BaggagePropagator(),
                        new OpenTelemetry.Extensions.Propagators.B3Propagator()
                    }));
                    tracing.AddNpgsql();
                    tracing.AddAspNetCoreInstrumentation();
                    tracing.AddHttpClientInstrumentation();
                    tracing.AddEntityFrameworkCoreInstrumentation();
                    tracing.AddRedisInstrumentation();
                    tracing.AddOtlpExporter();
                    
                })
            .WithMetrics(metrics =>
             {
                 metrics.AddAspNetCoreInstrumentation()
                 .AddMeter("Microsoft.AspNetCore.Hosting")
                 .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                 .AddConsoleExporter();
             }).WithLogging(logging =>
             {
                 
             });
        }
    }
}
