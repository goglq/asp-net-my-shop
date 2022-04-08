using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyShop.Core.Interfaces.Services;
using MyShop.SharedProject;

namespace MyShop.HttpApiServer.Filters;

public class IsBannedCheckFilter : IAsyncAuthorizationFilter
{
    private readonly IAccountService _accountService;

    public IsBannedCheckFilter(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var userIdStr = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userIdStr is null) return;

        var userId = Guid.Parse(userIdStr);
        
        if (await _accountService.CheckIsBannedById(userId))
        {
            context.Result = new ObjectResult(new ResponseMessage<object>("User is banned", false))
            {
                StatusCode = StatusCodes.Status403Forbidden,
            };
        }
    }
}