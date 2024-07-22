namespace Markis.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            
            _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");

            await _next(context);

            if (context.Response.StatusCode == 404)
            {
                throw new Exception("404 Not Found!");
            }
        }
    }
}
