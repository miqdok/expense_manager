using System.Windows.Controls;
using ExpenseManager.WPF.ViewModels;

namespace ExpenseManager.WPF.Views;

public partial class WalletsPage : Page
{
    public WalletsPage()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private async void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
    {
        if (DataContext is WalletsViewModel vm)
            await vm.LoadWalletsCommand.ExecuteAsync(null);
    }
}
