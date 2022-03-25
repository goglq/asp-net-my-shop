using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Infrastructure.Services.Accounts;
using MyShop.Models;
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

    [Authorize(Roles = "admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetAll()
    {
        return Ok(await _accountService.GetAccounts());
    }
    
    /// <summary>
    /// Registers Account With Unique Email
    /// </summary>
    /// <param name="registrationAccountDto">DTO that contains Name, Email, Password</param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<ActionResult<ResponseMessage<string>>> Registration(RegistrationAccountDto registrationAccountDto)
    {
        try
        {
            _logger.LogInformation("Registering new account...");
            var token = await _accountService.Register(registrationAccountDto);
            return Created(Uri.UriSchemeHttp, new ResponseMessage<string>("Register has succeeded", true, token));
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
    public async Task<ActionResult<ResponseMessage<string>>> Login(LoginAccountDto loginAccountDto)
    {
        try
        {
            var token = await _accountService.Login(loginAccountDto);
            return Ok(new ResponseMessage<string>("Login has succeeded", true, token));
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