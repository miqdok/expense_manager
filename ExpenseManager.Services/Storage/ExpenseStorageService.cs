using ExpenseManager.Storage.Models;
using ExpenseManager.UI.Models;

namespace ExpenseManager.Services.Storage;

public class ExpenseStorageService
{
    // Map storage entities to UI models
    public IReadOnlyList<WalletModel> GetWallets()
    {
        return FakeStorage.GetWallets()
            .Select(MapWallet)
            .ToList();
    }

    public IReadOnlyList<TransactionModel> GetTransactions(Guid walletId)
    {
        return FakeStorage.GetTransactions()
            .Where(transaction => transaction.WalletId == walletId)
            .OrderByDescending(transaction => transaction.Date)
            .Select(MapTransaction)
            .ToList();
    }

    private static WalletModel MapWallet(WalletEntity entity)
    {
        return new WalletModel(entity.Id, entity.Name, entity.Currency);
    }

    private static TransactionModel MapTransaction(TransactionEntity entity)
    {
        return new TransactionModel(
            entity.Id,
            entity.WalletId,
            entity.Amount,
            entity.Category,
            entity.Description,
            entity.Date);
    }
}
