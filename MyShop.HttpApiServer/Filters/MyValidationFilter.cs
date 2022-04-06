using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyShop.SharedProject;

namespace MyShop.HttpApiServer.Filters;

public class MyValidationFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result =
                new BadRequestObjectResult(
                    new ResponseMessage<ModelStateDictionary>("Validation Error", false, context.ModelState));
        }
    }
}