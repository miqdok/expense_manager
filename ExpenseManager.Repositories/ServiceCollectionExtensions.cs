using Microsoft.Extensions.DependencyInjection;

namespace ExpenseManager.Repositories;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IWalletRepository, WalletRepository>();
        services.AddSingleton<ITransactionRepository, TransactionRepository>();
        return services;
    }
}
