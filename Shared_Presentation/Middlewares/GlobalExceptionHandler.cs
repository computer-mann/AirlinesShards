using Microsoft.AspNetCore.Http;

namespace Shared_Presentation.Middlewares
{
    public class GlobalExceptionHandler : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //try
            //{

            //}catch (Exception ex)
            //{
            //    context.Response.StatusCode = 500;

            //}
            return Task.CompletedTask;
        }
    }
}
