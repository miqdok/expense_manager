using System.Windows.Controls;
using ExpenseManager.Services.Storage;
using ExpenseManager.UI.Models;

namespace ExpenseManager.WPF.Pages;

public partial class WalletDetailsPage : Page
{
    private readonly Frame _frame;
    private readonly WalletModel _wallet;

    public WalletDetailsPage(Frame frame, WalletModel wallet, IExpenseStorageService storageService)
    {
        InitializeComponent();
        _frame = frame;
        _wallet = wallet;

        if (!wallet.TransactionsLoaded)
        {
            wallet.SetTransactions(storageService.GetTransactions(wallet.Id));
        }

        WalletNameText.Text = wallet.Name;
        WalletIdText.Text = $"Id: {wallet.Id}";
        WalletCurrencyText.Text = $"Валюта: {wallet.Currency}";
        WalletTotalText.Text = $"Баланс: {wallet.TotalAmount:N2}";
        TransactionsListBox.ItemsSource = wallet.Transactions;
    }

    private void BackButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        _frame.GoBack();
    }

    private void TransactionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (TransactionsListBox.SelectedItem is TransactionModel transaction)
        {
            _frame.Navigate(new TransactionDetailsPage(_frame, transaction));
            TransactionsListBox.SelectedItem = null;
        }
    }
}
