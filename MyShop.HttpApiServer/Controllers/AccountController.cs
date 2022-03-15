using Microsoft.AspNetCore.Mvc;
using MyShop.Infrastructure.Services.Accounts;
using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiServer.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    
    private readonly ILogger<AccountController> _logger;

    public AccountController(IAccountService accountService, ILogger<AccountController> logger)
    {
        _accountService = accountService;
        _logger = logger;
    }
    
    
    [HttpPost("register")]
    public async Task<IActionResult> Registration(AccountDto accountDto)
    {
        _logger.LogInformation("Registering new account...");
        if (await _accountService.IsEmailTaken(accountDto.Email!))
            return BadRequest("Email is already taken");
        await _accountService.Register(accountDto);
        return Created(Uri.UriSchemeHttp, accountDto);
    }
}