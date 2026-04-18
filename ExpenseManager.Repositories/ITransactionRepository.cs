using ExpenseManager.Repositories.Models;

namespace ExpenseManager.Repositories;

public interface ITransactionRepository
{
    Task<IReadOnlyList<TransactionEntity>> GetByWalletIdAsync(Guid walletId);
    Task<TransactionEntity?> GetByIdAsync(Guid id);
    Task AddAsync(TransactionEntity transaction);
    Task UpdateAsync(TransactionEntity transaction);
    Task DeleteAsync(Guid id);
}
