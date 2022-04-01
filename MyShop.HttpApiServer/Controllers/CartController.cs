using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Core.Interfaces;
using MyShop.Core.Interfaces.Services;
using MyShop.Core.Models;
using MyShop.SharedProject;

namespace MyShop.HttpApiServer.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }
    
    [HttpGet("self")]
    public async Task<ActionResult<ResponseMessage<IEnumerable<CartItem>>>> GetOwnCart()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var guid = Guid.Parse(userId);
            var cartItems = await _cartService.GetCartByUserId(guid);
            return Ok(new ResponseMessage<IEnumerable<CartItem>>("User Cart", true, cartItems));
        }
        catch (Exception e)
        {
            return BadRequest(new ResponseMessage<ProblemDetails>(e.Message, false, new ProblemDetails()
            {
                Title = "Not Found",
                Status = 404
            }));
        }
    }

    [HttpPost("addProduct")]
    public async Task<ActionResult<ResponseMessage<string>>> AddProduct(string productId)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userGuid = Guid.Parse(userId);
            var productGuid = Guid.Parse(productId);
            await _cartService.AddProduct(userGuid, productGuid);
            return Ok(new ResponseMessage<string>("Product Added", true, "Success"));
        }
        catch (Exception e)
        {
            return BadRequest(new ResponseMessage<ProblemDetails>(e.Message, false, new ProblemDetails()
            {
                Title = "Bad Request",
                Status = 400
            }));
        }
    }
    
    
}