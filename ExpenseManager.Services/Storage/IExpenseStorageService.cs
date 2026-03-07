using ExpenseManager.UI.Models;

namespace ExpenseManager.Services.Storage;

public interface IExpenseStorageService
{
    IReadOnlyList<WalletModel> GetWallets();
    IReadOnlyList<TransactionModel> GetTransactions(Guid walletId);
}
