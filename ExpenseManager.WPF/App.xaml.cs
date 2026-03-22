using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using ExpenseManager.Services;
using ExpenseManager.WPF.Services;
using ExpenseManager.WPF.ViewModels;

namespace ExpenseManager.WPF;

public partial class App : Application
{
    public static ServiceProvider ServiceProvider { get; private set; } = null!;

    private void OnStartup(object sender, StartupEventArgs e)
    {
        var services = new ServiceCollection();

        services.AddAppServices();

        // navigation
        services.AddSingleton<NavigationService>();
        services.AddSingleton<INavigationService>(sp => sp.GetRequiredService<NavigationService>());

        // viewmodels
        services.AddTransient<WalletsViewModel>();
        services.AddTransient<WalletDetailsViewModel>();
        services.AddTransient<TransactionDetailsViewModel>();

        ServiceProvider = services.BuildServiceProvider();

        var mainWindow = new MainWindow();
        mainWindow.Show();
    }
}
