using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using ExpenseManager.Services.Storage;

namespace ExpenseManager.WPF.Pages;

public partial class WalletsPage : Page
{
    private readonly Frame _frame;
    private readonly IExpenseStorageService _storageService;

    public WalletsPage(Frame frame)
    {
        InitializeComponent();
        _frame = frame;
        _storageService = App.ServiceProvider.GetRequiredService<IExpenseStorageService>();
        var wallets = _storageService.GetWallets();
        foreach (var w in wallets)
        {
            if (!w.TransactionsLoaded)
            {
                w.SetTransactions(_storageService.GetTransactions(w.Id));
            }
        }
        WalletsListBox.ItemsSource = wallets;
    }

    private void WalletsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (WalletsListBox.SelectedItem is UI.Models.WalletModel wallet)
        {
            _frame.Navigate(new WalletDetailsPage(_frame, wallet, _storageService));
            WalletsListBox.SelectedItem = null;
        }
    }
}
