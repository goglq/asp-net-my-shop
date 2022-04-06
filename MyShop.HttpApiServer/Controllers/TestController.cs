using Microsoft.AspNetCore.Mvc;

namespace MyShop.HttpApiServer.Controllers;

[Route("[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet]
    public Task Index()
    {
        throw new Exception("This is unhandled exception");
    }
}