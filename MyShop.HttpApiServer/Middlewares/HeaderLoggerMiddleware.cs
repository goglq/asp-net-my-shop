using MyShop.Infrastructure.Repositories;

namespace MyShop.HttpApiServer.Middlewares;

public class HeaderLoggerMiddleware
{
    private readonly ILogger<HeaderLoggerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public HeaderLoggerMiddleware(ILogger<HeaderLoggerMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("Request Header: {RequestHeaders}", context.Request.Headers);
        await _next(context);
        _logger.LogInformation("Response Header: {ResponseHeaders}", context.Response.Headers);
    }
}