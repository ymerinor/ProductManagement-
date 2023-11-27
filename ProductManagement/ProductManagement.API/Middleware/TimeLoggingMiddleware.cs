using System.Diagnostics;

namespace ProductManagement.API.Middleware
{
    public class TimeLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TimeLoggingMiddleware> _logger;

        public TimeLoggingMiddleware(RequestDelegate next, ILogger<TimeLoggingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await _next(context);

            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            LogResponseTime(context, elapsedMilliseconds);
        }

        private void LogResponseTime(HttpContext context, long elapsedMilliseconds)
        {
            _logger.LogInformation("{Path} - Elapsed Time: {ElapsedMilliseconds} ms", context.Request.Path, elapsedMilliseconds);
        }
    }

    public static class ResponseTimeLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseTimeLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TimeLoggingMiddleware>();
        }
    }
}
