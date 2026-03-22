using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using ExpenseManager.WPF.Services;
using ExpenseManager.WPF.ViewModels;

namespace ExpenseManager.WPF;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var navigationService = App.ServiceProvider.GetRequiredService<NavigationService>();
        navigationService.SetFrame(MainFrame);
        navigationService.NavigateTo<WalletsViewModel>();
    }
}
