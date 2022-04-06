using MyShop.Core.Interfaces;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Infrastructure.Databases;
using MyShop.Infrastructure.Repositories;

namespace MyShop.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IAsyncDisposable
{
    private readonly AppDbContext _context;
    
    public IAccountRepository AccountRepository { get; }
    
    public IProductRepository ProductRepository { get; }
    
    public ICartRepository CartRepository { get; }
    
    public ICartItemRepository CartItemRepository { get; }
    
    public IConfirmationCodeRepository ConfirmationCodeRepository { get; }

    public ICategoryRepository CategoryRepository { get; }

    public UnitOfWork(
        IAccountRepository accountRepository, 
        IProductRepository productRepository, 
        ICategoryRepository categoryRepository, 
        ICartRepository cartRepository, 
        ICartItemRepository cartItemRepository, 
        IConfirmationCodeRepository confirmationCodeRepository,
        AppDbContext context)
    {
        AccountRepository = accountRepository;
        ProductRepository = productRepository;
        CategoryRepository = categoryRepository;
        CartRepository = cartRepository;
        CartItemRepository = cartItemRepository;
        ConfirmationCodeRepository = confirmationCodeRepository;
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => _context.SaveChangesAsync(cancellationToken);

    public void Dispose() => _context.Dispose();

    public ValueTask DisposeAsync() => _context.DisposeAsync();
}