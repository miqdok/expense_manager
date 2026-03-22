using ExpenseManager.Repositories;
using ExpenseManager.Services.Dto;

namespace ExpenseManager.Services;

public class WalletService : IWalletService
{
    private readonly IWalletRepository _walletRepository;
    private readonly ITransactionRepository _transactionRepository;

    public WalletService(IWalletRepository walletRepository, ITransactionRepository transactionRepository)
    {
        _walletRepository = walletRepository;
        _transactionRepository = transactionRepository;
    }

    public IReadOnlyList<WalletListDto> GetWalletList()
    {
        return _walletRepository.GetAll()
            .Select(w => new WalletListDto(w.Id, w.Name, w.Currency))
            .ToList();
    }

    public WalletDetailsDto? GetWalletDetails(Guid id)
    {
        var wallet = _walletRepository.GetById(id);
        if (wallet == null)
            return null;

        var total = _transactionRepository.GetByWalletId(id).Sum(t => t.Amount);
        return new WalletDetailsDto(wallet.Id, wallet.Name, wallet.Currency, total);
    }
}
