using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Middleware
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
            catch (Exception exception)
            {
                var (statusCode, title) = exception switch
                {
                    ArgumentNullException or ArgumentException => (StatusCodes.Status400BadRequest, "Bad Request"),
                    KeyNotFoundException => (StatusCodes.Status404NotFound, "Not Found"),
                    _ => (StatusCodes.Status500InternalServerError, "Server Error")
                };

                if(statusCode == StatusCodes.Status500InternalServerError)
                {
                    _logger.LogError(exception, "An unhandled exception occurred during the request: {Path} {Method}",
                        context.Request.Path,
                        context.Request.Method);
                }

                var problemDetails = new ProblemDetails
                {
                    Status = statusCode,
                    Title = title,
                    Detail = exception.Message,
                    Instance = context.Request.Path 
                };

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}