using Microsoft.Extensions.DependencyInjection;
using ExpenseManager.Repositories;

namespace ExpenseManager.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, string dbPath)
    {
        services.AddRepositories(dbPath);
        services.AddSingleton<IWalletService, WalletService>();
        services.AddSingleton<ITransactionService, TransactionService>();
        return services;
    }
}
