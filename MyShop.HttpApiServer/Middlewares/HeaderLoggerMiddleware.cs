namespace MyShop.HttpApiServer.Middlewares;

public class HeaderLoggerMiddleware : IMiddleware
{
    private readonly ILogger<HeaderLoggerMiddleware> _logger;

    public HeaderLoggerMiddleware(ILogger<HeaderLoggerMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _logger.LogInformation("Request Header: {RequestHeaders}", context.Request.Headers);
        await next(context);
        _logger.LogInformation("Response Header: {ResponseHeaders}", context.Response.Headers);
    }
}