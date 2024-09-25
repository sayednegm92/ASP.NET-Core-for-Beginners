using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace FirstApp.Filters
{
    public class LogActivityFilter :IAsyncActionFilter
    {
        private readonly ILogger<LogActivityFilter> _logger;

        public LogActivityFilter(ILogger<LogActivityFilter> logger)
        {
            _logger = logger;
        }
        //public void OnActionExecuting(ActionExecutingContext context)
        //{
        //    _logger.LogInformation($"Executing Action {context.ActionDescriptor.DisplayName} on Controller Name {context.Controller} with Arguments {JsonSerializer.Serialize(context.ActionArguments)}");
        //}
        //public void OnActionExecuted(ActionExecutedContext context)
        //{
        //    _logger.LogInformation($"Action {context.ActionDescriptor.DisplayName} Finished Executing on Controller Name {context.Controller}");
        //}

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation($"(Async)Executing Action {context.ActionDescriptor.DisplayName} on Controller Name {context.Controller} with Arguments {JsonSerializer.Serialize(context.ActionArguments)}");
            await next();
            _logger.LogInformation($"Action {context.ActionDescriptor.DisplayName} Finished Executing on Controller Name {context.Controller}");
        }
    }
}
