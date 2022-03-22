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
    public async Task<ActionResult<ResponseMessage<ProblemDetails>>> Registration(RegistrationAccountDto registrationAccountDto)
    {
        try
        {
            _logger.LogInformation("Registering new account...");
            await _accountService.Register(registrationAccountDto);
            return Created(Uri.UriSchemeHttp, new ResponseMessage<ProblemDetails>("Register has succeeded", true));
        }
        catch (Exception e)
        {
            return BadRequest(new ResponseMessage<ProblemDetails>(e.Message, false, new ProblemDetails()
            {
                Title = "Bad Request",
                Status = 404,
            }));
        }
    }

    /// <summary>
    /// Logins Into the Account that has been sent 
    /// </summary>
    /// <param name="loginAccountDto">DTO that contains Email and Password</param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<ActionResult<ResponseMessage<ProblemDetails>>> Login(LoginAccountDto loginAccountDto)
    {
        try
        {
            await _accountService.Login(loginAccountDto);
            return Ok(new ResponseMessage<ProblemDetails>("Login has succeeded", true));
        }
        catch (Exception e) when (e is NullReferenceException or ArgumentException)
        {
            return Unauthorized(new ResponseMessage<ProblemDetails>(e.Message, false, new ProblemDetails()
            {
                Title = "Unauthorized",
                Status = 401,
            }));
        }
    }
}