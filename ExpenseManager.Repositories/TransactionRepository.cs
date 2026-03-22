using ExpenseManager.Repositories.Models;
using ExpenseManager.Repositories.Storage;

namespace ExpenseManager.Repositories;

public class TransactionRepository : ITransactionRepository
{
    public IReadOnlyList<TransactionEntity> GetByWalletId(Guid walletId)
    {
        return FakeStorage.GetTransactions()
            .Where(t => t.WalletId == walletId)
            .OrderByDescending(t => t.Date)
            .ToList();
    }

    public TransactionEntity? GetById(Guid id)
    {
        return FakeStorage.GetTransactions().FirstOrDefault(t => t.Id == id);
    }
}
