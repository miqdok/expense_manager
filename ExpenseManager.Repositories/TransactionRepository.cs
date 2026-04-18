using Microsoft.EntityFrameworkCore;
using ExpenseManager.Repositories.Data;
using ExpenseManager.Repositories.Models;

namespace ExpenseManager.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public TransactionRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IReadOnlyList<TransactionEntity>> GetByWalletIdAsync(Guid walletId)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Transactions
            .Where(t => t.WalletId == walletId)
            .OrderByDescending(t => t.Date)
            .ToListAsync();
    }

    public async Task<TransactionEntity?> GetByIdAsync(Guid id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Transactions.FindAsync(id);
    }

    public async Task AddAsync(TransactionEntity transaction)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        context.Transactions.Add(transaction);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TransactionEntity transaction)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        context.Transactions.Update(transaction);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var transaction = await context.Transactions.FindAsync(id);

        if (transaction != null)
        {
            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync();
        }
    }
}
