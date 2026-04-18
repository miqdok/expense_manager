using ExpenseManager.Repositories;
using ExpenseManager.Repositories.Enums;
using ExpenseManager.Repositories.Models;
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

    public async Task<IReadOnlyList<WalletListDto>> GetWalletListAsync()
    {
        var wallets = await _walletRepository.GetAllAsync();
        return wallets
            .Select(w => new WalletListDto(w.Id, w.Name, w.Currency))
            .ToList();
    }

    public async Task<WalletDetailsDto?> GetWalletDetailsAsync(Guid id)
    {
        var wallet = await _walletRepository.GetByIdAsync(id);
        if (wallet == null)
            return null;

        var transactions = await _transactionRepository.GetByWalletIdAsync(id);
        var total = transactions.Sum(t => t.Amount);
        return new WalletDetailsDto(wallet.Id, wallet.Name, wallet.Currency, total);
    }

    public async Task AddWalletAsync(string name, Currency currency)
    {
        var wallet = new WalletEntity(name, currency);
        await _walletRepository.AddAsync(wallet);
    }

    public async Task UpdateWalletAsync(Guid id, string name, Currency currency)
    {
        var wallet = await _walletRepository.GetByIdAsync(id);
        if (wallet == null)
            return;

        wallet.Name = name;
        wallet.Currency = currency;
        await _walletRepository.UpdateAsync(wallet);
    }

    public async Task DeleteWalletAsync(Guid id)
    {
        await _walletRepository.DeleteAsync(id);
    }
}
