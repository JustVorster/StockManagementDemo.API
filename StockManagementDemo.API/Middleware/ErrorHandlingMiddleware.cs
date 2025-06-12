using System.Net;
using System.Text.Json;

namespace StockManagementDemo.API.Middleware
{
    public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new { error = "An unexpected error occurred." };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}