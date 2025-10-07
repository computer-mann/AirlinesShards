using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AirlinesApi.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogCritical(exception,"Serious unexpected fault: {message}",exception.Message);

            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails()
            {
                Status=(int)HttpStatusCode.InternalServerError,
                Title="Internal Server Error",
                Detail="Something happened on our end. Try again",
                Type="https://tools.ietf.org/html/rfc7231#section-6.6.1"
            });
            return true;
        }
    }
}
