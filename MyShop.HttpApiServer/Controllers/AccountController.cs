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
    
    /// <summary>
    /// Registers Account With Unique Email
    /// </summary>
    /// <param name="registrationAccountDto">DTO that contains Name, Email, Password</param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> Registration(RegistrationAccountDto registrationAccountDto)
    {
        _logger.LogInformation("Registering new account...");
        if (await _accountService.IsEmailTaken(registrationAccountDto.Email!))
            return BadRequest("Email is already taken");
        await _accountService.Register(registrationAccountDto);
        return Created(Uri.UriSchemeHttp, registrationAccountDto);
    }

    /// <summary>
    /// Logins Into the Account that has been sent 
    /// </summary>
    /// <param name="loginAccountDto">DTO that contains Email and Password</param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<ActionResult<ResponseMessage<string>>> Login(LoginAccountDto loginAccountDto)
    {
        try
        {
            await _accountService.Login(loginAccountDto);
            return Ok(new ResponseMessage<string>("Login has succeeded", true));
        }
        catch (Exception ex) when (ex is NullReferenceException | ex is ArgumentException)
        {
            return Unauthorized(new ResponseMessage<string>(ex.Message, false));
        }
    }
}