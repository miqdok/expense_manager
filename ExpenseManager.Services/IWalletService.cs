using ExpenseManager.Repositories.Enums;
using ExpenseManager.Services.Dto;

namespace ExpenseManager.Services;

public interface IWalletService
{
    Task<IReadOnlyList<WalletListDto>> GetWalletListAsync();
    Task<WalletDetailsDto?> GetWalletDetailsAsync(Guid id);
    Task AddWalletAsync(string name, Currency currency);
    Task UpdateWalletAsync(Guid id, string name, Currency currency);
    Task DeleteWalletAsync(Guid id);
}
