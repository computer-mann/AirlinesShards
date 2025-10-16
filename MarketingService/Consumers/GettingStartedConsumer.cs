using MarketingService.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MarketingService.Consumers
{
    public class GettingStartedConsumer :
    IConsumer<GettingStarted>
    {
        readonly ILogger<GettingStartedConsumer> _logger;

        public GettingStartedConsumer(ILogger<GettingStartedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<GettingStarted> context)
        {
            _logger.LogInformation("Received Text: {Text}", JsonSerializer.Serialize(context.Message));
            await Task.Delay(1000);
        }
    }
}
