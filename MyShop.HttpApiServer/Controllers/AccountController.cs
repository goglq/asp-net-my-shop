using Microsoft.AspNetCore.Mvc;
using MyShop.Infrastructure.Services.Accounts;
using MyShop.SharedProject;
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
    public async Task<IActionResult> Registration(RegistrationAccountDto registrationAccountDto)
    {
        _logger.LogInformation("Registering new account...");
        if (await _accountService.IsEmailTaken(registrationAccountDto.Email!))
            return BadRequest("Email is already taken");
        await _accountService.Register(registrationAccountDto);
        return Created(Uri.UriSchemeHttp, registrationAccountDto);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ResponseMessage<string>>> Login(LoginAccountDto loginAccountDto)
    {
        try
        {
            await _accountService.Login(loginAccountDto);
            return Ok(new ResponseMessage<string>("Login has succeeded", true));
        }
        catch (NullReferenceException ex)
        {
            return Unauthorized(new ResponseMessage<string>(ex.Message, false));
        }
        catch (ArgumentException ex)
        {
            return Unauthorized(new ResponseMessage<string>(ex.Message, false));
        }
    }
}