using System.Windows.Controls;
using ExpenseManager.WPF.ViewModels;

namespace ExpenseManager.WPF.Views;

public partial class TransactionDetailsPage : Page
{
    public TransactionDetailsPage()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private async void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
    {
        if (DataContext is TransactionDetailsViewModel vm)
            await vm.LoadTransactionCommand.ExecuteAsync(null);
    }
}
