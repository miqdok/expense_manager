using Microsoft.EntityFrameworkCore;
using ExpenseManager.Repositories.Data;
using ExpenseManager.Repositories.Models;

namespace ExpenseManager.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public WalletRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IReadOnlyList<WalletEntity>> GetAllAsync()
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Wallets.ToListAsync();
    }

    public async Task<WalletEntity?> GetByIdAsync(Guid id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Wallets.FindAsync(id);
    }

    public async Task AddAsync(WalletEntity wallet)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        context.Wallets.Add(wallet);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(WalletEntity wallet)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        context.Wallets.Update(wallet);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var wallet = await context.Wallets
            .Include(w => w.Transactions)
            .FirstOrDefaultAsync(w => w.Id == id);

        if (wallet != null)
        {
            context.Wallets.Remove(wallet);
            await context.SaveChangesAsync();
        }
    }
}
