using MyShop.Core.Interfaces.Repositories;

namespace MyShop.Core.Interfaces;

public interface IUnitOfWork
{
    IAccountRepository AccountRepository { get; }
    
    ICategoryRepository CategoryRepository { get; }
    
    IProductRepository ProductRepository { get; }
    
    ICartRepository CartRepository { get; }
    
    ICartItemRepository CartItemRepository { get; }
    
    IConfirmationCodeRepository ConfirmationCodeRepository { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}