using System.IO;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using ExpenseManager.Repositories.Data;
using ExpenseManager.Services;
using ExpenseManager.WPF.Services;
using ExpenseManager.WPF.ViewModels;

namespace ExpenseManager.WPF;

public partial class App : Application
{
    public static ServiceProvider ServiceProvider { get; private set; } = null!;

    private async void OnStartup(object sender, StartupEventArgs e)
    {
        var dbFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "ExpenseManager");
        Directory.CreateDirectory(dbFolder);
        var dbPath = Path.Combine(dbFolder, "expenses.db");

        var services = new ServiceCollection();

        services.AddAppServices(dbPath);

        // navigation
        services.AddSingleton<NavigationService>();
        services.AddSingleton<INavigationService>(sp => sp.GetRequiredService<NavigationService>());

        // viewmodels
        services.AddTransient<WalletsViewModel>();
        services.AddTransient<WalletDetailsViewModel>();
        services.AddTransient<TransactionDetailsViewModel>();
        services.AddTransient<WalletEditViewModel>();
        services.AddTransient<TransactionEditViewModel>();

        ServiceProvider = services.BuildServiceProvider();

        // init db
        var contextFactory = ServiceProvider.GetRequiredService<Microsoft.EntityFrameworkCore.IDbContextFactory<AppDbContext>>();
        using var context = await contextFactory.CreateDbContextAsync();
        await context.Database.EnsureCreatedAsync();
        await DbSeeder.SeedAsync(context);

        var mainWindow = new MainWindow();
        mainWindow.Show();
    }
}
