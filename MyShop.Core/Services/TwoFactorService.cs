using Microsoft.Extensions.Logging;
using MyShop.Core.Interfaces;
using MyShop.Core.Interfaces.Services;
using MyShop.Core.Models;

namespace MyShop.Core.Services;

public class TwoFactorService : ITwoFactorService
{
    private readonly ILogger<TwoFactorService> _logger;

    private readonly IUnitOfWork _unitOfWork;
    
    private readonly Random _random = new Random();

    public TwoFactorService(ILogger<TwoFactorService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public int GenerateCode(int length) => _random.Next((int)Math.Pow(10, length - 1), (int)Math.Pow(10, length) - 1);
    
    public async Task<Guid> CreateCode(Guid accountId, int code)
    {
        var confirmationGuid = Guid.NewGuid();
        var confirmationCode = new ConfirmationCode()
        {
            Id = confirmationGuid,
            AccountId = accountId,
            Code = code,
            CreationDate = DateTime.Now
        };
        await _unitOfWork.ConfirmationCodeRepository.Add(confirmationCode);
        await _unitOfWork.SaveChangesAsync();
        return confirmationGuid;
    }

    public async Task<bool> IsCorrectCode(Guid codeId, int code)
    {
        var confirmationCode = await _unitOfWork.ConfirmationCodeRepository.GetById(codeId);
        return !(DateTime.Now > confirmationCode.CreationDate.Add(TimeSpan.FromMinutes(2)) || confirmationCode.Code != code);
    }
}