using MarketingService;
using MarketingService.Consumers;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddHostedService<Worker>();
var services = builder.Services;

var host = builder.Build();
host.Run();
