using System.Text.Json;

namespace GameVault.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
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
            catch (KeyNotFoundException ex)
            {
                await WriteError(context, 404, ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                await WriteError(context, 401, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                await WriteError(context, 400, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                await WriteError(context, 500, "An internal error occurred.");
            }
        }

        private static async Task WriteError(
            HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = new { error = message };
            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}