using ExpenseManager.Repositories.Models;

namespace ExpenseManager.Repositories;

public interface IWalletRepository
{
    IReadOnlyList<WalletEntity> GetAll();
    WalletEntity? GetById(Guid id);
}
