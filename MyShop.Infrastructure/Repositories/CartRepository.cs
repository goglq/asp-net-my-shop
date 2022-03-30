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
        var cart = await Entities.Include(cart => cart.CartItems).FirstAsync(cart => cart.AccountId == id);
        return cart.CartItems.ToList();
    }
}