using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyShop.SharedProject;

namespace MyShop.HttpApiServer.Filters;

public class MyShopExceptionFilter : Attribute, IExceptionFilter
{
    private readonly ILogger<MyShopExceptionFilter> _logger;

    private readonly IHostEnvironment _hostEnvironment;

    public MyShopExceptionFilter(
        ILogger<MyShopExceptionFilter> logger, 
        IHostEnvironment hostEnvironment)
    {
        _logger = logger;
        _hostEnvironment = hostEnvironment;
    }
    
    public void OnException(ExceptionContext context)
    {
        if (_hostEnvironment.IsDevelopment())
        {
            context.Result =
                new ObjectResult(new ResponseMessage<object>("Detailed Unhandled Error", false, new {context.Exception.Message, context.Exception.StackTrace} ));
            return;
        }
        context.Result = new ObjectResult(new ResponseMessage<string>("Unhandled Error", false));
    }
}