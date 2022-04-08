using Microsoft.AspNetCore.Mvc.Filters;
using MyShop.Core.Interfaces.Services;

namespace MyShop.HttpApiServer.Filters;

public class IsBannedCheckFilterFactory : Attribute, IFilterFactory
{
    public bool IsReusable => false;

    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
    {
        var accountService = serviceProvider.GetRequiredService<IAccountService>();
        return new IsBannedCheckFilter(accountService);
    }
}