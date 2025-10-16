using MarketingService;
using MarketingService.Consumers;
using MarketingService.Models;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.Serialization.Json;

var builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddHostedService<Worker>();
var services = builder.Services;
services.AddRebus(configure => configure
    .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost:5672", "marketing"))
    .Serialization(s => s.UseSystemTextJson())
    .Options(o =>
    {
        o.SetNumberOfWorkers(1);
        o.SetMaxParallelism(1);
    })
);

services.AutoRegisterHandlersFromAssemblyOf<GettingStartedConsumer>();

var host = builder.Build();
host.Run();
