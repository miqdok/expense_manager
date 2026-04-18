using ExpenseManager.Repositories.Models;

namespace ExpenseManager.Repositories;

public interface IWalletRepository
{
    Task<IReadOnlyList<WalletEntity>> GetAllAsync();
    Task<WalletEntity?> GetByIdAsync(Guid id);
    Task AddAsync(WalletEntity wallet);
    Task UpdateAsync(WalletEntity wallet);
    Task DeleteAsync(Guid id);
}
