namespace Markis.Middlewares
{
    public class DomainExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public DomainExceptionMiddleware(RequestDelegate next, ILogger<DomainExceptionMiddleware> logger)
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
                const string message = "You got an error!";
                _logger.LogError(exception, message);

                context.Response.Clear();

                string imageUrl = "/img/404.png";
                string htmlContent = $@"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Full Screen Image</title>
                <style>
                    body, html {{
                        margin: 0;
                        padding: 0;
                        height: 100%;
                        overflow: hidden;
                        display: flex;
                        justify-content: center;
                        align-items: center;
                    }}
                    img {{
                        max-width: 100%;
                        max-height: 100%;
                        object-fit: contain;
                    }}
                </style>
            </head>
            <body>
                <img src=""{imageUrl}"" alt=""Full Screen Image"">
            </body>
            </html>";

                await context.Response.WriteAsync(htmlContent);
            }

        }
    }
}
