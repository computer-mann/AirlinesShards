using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace AirlinesWeb.Filters
{
    public class LogRequestTimeAndDurationActionFilter : IActionFilter
    {
        private readonly ILogger<LogRequestTimeAndDurationActionFilter> logger;
        private readonly Stopwatch stopwatch;

        public LogRequestTimeAndDurationActionFilter(ILogger<LogRequestTimeAndDurationActionFilter> logger)
        {
            this.logger = logger;
            stopwatch = Stopwatch.StartNew();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogInformation("Request Duration for {0} took {1}ms", context.ActionDescriptor.DisplayName, stopwatch.ElapsedMilliseconds);
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
            logger.LogInformation("{0} request time => {1}", context.ActionDescriptor.DisplayName,DateTime.Now);
        }
    }
}
