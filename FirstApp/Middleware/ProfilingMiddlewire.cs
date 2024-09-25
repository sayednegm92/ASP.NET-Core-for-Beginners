using System.Diagnostics;

namespace FirstApp.Middleware
{
    public class ProfilingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ProfilingMiddleware> _logger;

        public ProfilingMiddleware(RequestDelegate next,ILogger<ProfilingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context) {
            var stopwach=new Stopwatch();
            stopwach.Start();
           await _next(context);
            stopwach.Stop();
            _logger.LogInformation($"Request `{context.Request.Path}` Took `{stopwach.ElapsedMilliseconds} ms` To Execute");
        }
    }
}
