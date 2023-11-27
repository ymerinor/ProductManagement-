using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace ProductManagement.API.Middleware
{
    [ExcludeFromCodeCoverage]
    public class TimeLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TimeLoggingMiddleware> _logger;

        [ExcludeFromCodeCoverage]
        public TimeLoggingMiddleware(RequestDelegate next, ILogger<TimeLoggingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [ExcludeFromCodeCoverage]
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
            _logger.LogInformation("{Path} - Tiempo Transacurrdio: {ElapsedMilliseconds} ms", context.Request.Path, elapsedMilliseconds);
        }
    }

    [ExcludeFromCodeCoverage]
    public static class ResponseTimeLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseTimeLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TimeLoggingMiddleware>();
        }
    }
}
