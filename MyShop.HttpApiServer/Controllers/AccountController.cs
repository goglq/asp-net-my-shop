using Microsoft.AspNetCore.Mvc;
using MyShop.SharedProject.DTOs;

namespace MyShop.HttpApiServer.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("registration")]
    public async Task<IActionResult> Registration(AccountDto accountDto)
    {
        return Ok();
    }
}