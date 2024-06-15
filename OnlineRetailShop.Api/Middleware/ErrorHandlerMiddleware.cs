using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using OnlineRetailShop.Infrastructure.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace OnlineRetailShop.Api.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            context.Response.ContentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;
            var result = new { error = exception.Message };

            if (exception is NotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
            }
            else if (exception is ValidationException)
            {
                statusCode = HttpStatusCode.BadRequest;
            }

            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsJsonAsync(result);
        }
    }
}
