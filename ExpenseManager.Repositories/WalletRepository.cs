using ExpenseManager.Repositories.Models;
using ExpenseManager.Repositories.Storage;

namespace ExpenseManager.Repositories;

public class WalletRepository : IWalletRepository
{
    public IReadOnlyList<WalletEntity> GetAll()
    {
        return FakeStorage.GetWallets();
    }

    public WalletEntity? GetById(Guid id)
    {
        return FakeStorage.GetWallets().FirstOrDefault(w => w.Id == id);
    }
}
