using Microsoft.AspNetCore.Mvc.Filters;

namespace MyShop.HttpApiServer.Filters;

public class ActionParameterLoggerFilter : IActionFilter
{
    private readonly ILogger<ActionParameterLoggerFilter> _logger;

    public ActionParameterLoggerFilter(ILogger<ActionParameterLoggerFilter> logger)
    {
        _logger = logger;
    }
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation(
            "{ControllerName}/{ActionName} Parameters: {Parameters}", 
            context.RouteData.Values["controller"], 
            context.RouteData.Values["action"], 
            context.ActionArguments);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }
}