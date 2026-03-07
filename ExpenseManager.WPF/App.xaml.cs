using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using ExpenseManager.Services.Storage;

namespace ExpenseManager.WPF;

public partial class App : Application
{
    public static ServiceProvider ServiceProvider { get; private set; } = null!;

    private void OnStartup(object sender, StartupEventArgs e)
    {
        var services = new ServiceCollection();
        services.AddSingleton<IExpenseStorageService, ExpenseStorageService>();
        ServiceProvider = services.BuildServiceProvider();

        var mainWindow = new MainWindow();
        mainWindow.Show();
    }
}
