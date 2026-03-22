using ExpenseManager.Repositories.Models;

namespace ExpenseManager.Repositories;

public interface ITransactionRepository
{
    IReadOnlyList<TransactionEntity> GetByWalletId(Guid walletId);
    TransactionEntity? GetById(Guid id);
}
