using System.Windows.Controls;
using ExpenseManager.WPF.ViewModels;

namespace ExpenseManager.WPF.Views;

public partial class WalletDetailsPage : Page
{
    public WalletDetailsPage()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private async void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
    {
        if (DataContext is WalletDetailsViewModel vm)
            await vm.LoadDataCommand.ExecuteAsync(null);
    }
}
