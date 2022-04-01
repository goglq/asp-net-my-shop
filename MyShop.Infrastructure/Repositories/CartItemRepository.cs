using Microsoft.EntityFrameworkCore;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Models;
using MyShop.Infrastructure.Databases;

namespace MyShop.Infrastructure.Repositories;

public class CartItemRepository : EfRepository<CartItem>, ICartItemRepository
{
    public CartItemRepository(AppDbContext dbContext) : base(dbContext) { }
    
    public async Task AddProduct(Cart cart, Guid productId)
    {
        
        if (cart.CartItems.Any(item => item.ProductId == productId))
        {
            var cartItem = await Entities.FirstAsync(cartItem => cartItem.ProductId == productId);
            cartItem.Count++;
        }
        else
        {
            var newCartItem = new CartItem()
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                CartId = cart.Id,
                Count = 1
            };
            Entities.Add(newCartItem);
        }
    }
}