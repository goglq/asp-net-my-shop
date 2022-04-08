using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyShop.HttpApiServer.Filters;

public class ProfileActionFilter : IActionFilter
{
    private readonly ILogger<ProfileActionFilter> _logger;
    
    private readonly Stopwatch _stopwatch;

    public ProfileActionFilter(ILogger<ProfileActionFilter> logger)
    {
        _logger = logger;
        _stopwatch = new Stopwatch();
    }
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        _stopwatch.Start();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch.Stop();
        _logger.LogInformation("Action Elapsed Time Benchmark: {Elapsed}", _stopwatch.Elapsed);
    }
}