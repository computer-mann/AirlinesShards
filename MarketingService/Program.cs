using MarketingService;
using MarketingService.Consumers;

var builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddHostedService<Worker>();
var services = builder.Services;

var host = builder.Build();
host.Run();
