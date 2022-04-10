using Microsoft.EntityFrameworkCore;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Models;
using MyShop.Infrastructure.Databases;

namespace MyShop.Infrastructure.Repositories;

public class CartRepository : EfRepository<Cart>, ICartRepository
{
    public CartRepository(AppDbContext dbContext) : base(dbContext)
    {
        
    }

    public async Task<IEnumerable<CartItem>> GetCartItemsByUserId(Guid id)
    {
        var cart = await Entities.Include(cart => cart.CartItems).ThenInclude(cartItem => cartItem.Product).FirstAsync(cart => cart.AccountId == id);
        return cart.CartItems.ToList();
    }

    public Task<Cart> GetCartByUserId(Guid id) => 
        Entities.Include(cart => cart.CartItems).FirstAsync(cart => cart.AccountId == id);
    
    public async Task AddProduct(Guid accountId, Guid productId)
    {
        var cart = await Entities.Include(cart => cart.CartItems).FirstAsync(cart => cart.AccountId == accountId);

        if (cart.CartItems.Any() && cart.CartItems.Any(cartItem => cartItem.ProductId == productId))
        {
            var cartItem = cart.CartItems.First(cartItem => cartItem.ProductId == productId);
            cartItem.Count++;
        }
        else
        {
            var newCartItem = new CartItem()
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                Count = 1
            };
            cart.CartItems.Add(newCartItem);
        }
        
        Update(cart);
    }
}