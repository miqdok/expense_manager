using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ExpenseManager.Repositories.Data;

namespace ExpenseManager.Repositories;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, string dbPath)
    {
        services.AddDbContextFactory<AppDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

        services.AddSingleton<IWalletRepository, WalletRepository>();
        services.AddSingleton<ITransactionRepository, TransactionRepository>();
        return services;
    }
}
