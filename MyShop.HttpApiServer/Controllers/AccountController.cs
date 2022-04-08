using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Core.Interfaces.Services;
using MyShop.Core.Models;
using MyShop.HttpApiServer.Filters;
using MyShop.SharedProject;
using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiServer.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    private readonly ITwoFactorService _twoFactorService;
    
    private readonly ILogger<AccountController> _logger;

    public AccountController(IAccountService accountService, ITwoFactorService twoFactorService, ILogger<AccountController> logger)
    {
        _accountService = accountService;
        _logger = logger;
        _twoFactorService = twoFactorService;
    }

    [Authorize(Roles = "admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetAll()
    {
        return Ok(await _accountService.GetAccounts());
    }

    /// <param name="registrationAccountDto">DTO that contains Name, Email, Password</param>
    /// <summary>
    ///     Registers Account With Unique Email
    /// </summary>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<ActionResult<ResponseMessage<string>>> Registration(RegistrationAccountDto registrationAccountDto)
    {
        try
        {
            _logger.LogInformation("Registering new account...");
            var token = await _accountService.Register(registrationAccountDto.Email, registrationAccountDto.Name, registrationAccountDto.Password);
            return Created(Uri.UriSchemeHttp, new ResponseMessage<string>("Register has succeeded", true, token));
        }
        catch (Exception e)
        {
            return BadRequest(new ResponseMessage<ProblemDetails>(e.Message, false, new ProblemDetails()
            {
                Title = "Not Found",
                Status = StatusCodes.Status404NotFound,
            }));
        }
    }

    /// <summary>
    /// Logins Into the Account that has been sent 
    /// </summary>
    /// <param name="loginAccountDto">DTO that contains Email and Password</param>
    /// <returns></returns>
    [HttpPost("login")]
    [TypeFilter(typeof(ActionParameterLoggerFilter))]
    public async Task<ActionResult<ResponseMessage<string>>> Login(LoginAccountDto loginAccountDto)
    {
        try
        {
            var token = await _accountService.Login(loginAccountDto.Email, loginAccountDto.Password);
            return Ok(new ResponseMessage<string>("Login has succeeded", true, token));
        }
        catch (Exception e) when (e is NullReferenceException or ArgumentException)
        {
            return Unauthorized(new ResponseMessage<ProblemDetails>(e.Message, false, new ProblemDetails()
            {
                Title = "Unauthorized",
                Status = StatusCodes.Status401Unauthorized,
            }));
        }
    }

    [HttpGet("loginTwoFactor")]
    public async Task<ActionResult<ResponseMessage<Guid>>> LoginTwoFactor(LoginAccountDto loginAccountDto)
    {
        try
        {
            var userId = await _accountService.LoginTwoFactor(loginAccountDto.Email, loginAccountDto.Password);
            var code = _twoFactorService.GenerateCode(6);
            var codeGuid = await _twoFactorService.CreateCode(userId, code);
            return Ok(new ResponseMessage<Guid>("Two Factor Code", true, codeGuid));
        }
        catch (Exception e)
        {
            return BadRequest(new ResponseMessage<ProblemDetails>(e.Message, false, new ProblemDetails()
            {
                Title = "Bad Request",
                Status = StatusCodes.Status400BadRequest
            }));
        }
    }

    [HttpPost("confirmCode")]
    public async Task<ActionResult<ResponseMessage<string>>> ConfirmTwoFactor(Guid codeId, int code)
    {
        try
        {
            var isCorrect = await _twoFactorService.IsCorrectCode(codeId, code);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new ResponseMessage<ProblemDetails>(e.Message, false, new ProblemDetails()
            {
                Title = "Bad Request",
                Status = StatusCodes.Status400BadRequest
            }));
        }
    }
}