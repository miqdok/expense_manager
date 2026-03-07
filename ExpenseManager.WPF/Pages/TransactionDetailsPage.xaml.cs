using System.Windows.Controls;
using System.Windows.Media;
using ExpenseManager.UI.Models;

namespace ExpenseManager.WPF.Pages;

public partial class TransactionDetailsPage : Page
{
    private readonly Frame _frame;

    public TransactionDetailsPage(Frame frame, TransactionModel transaction)
    {
        InitializeComponent();
        _frame = frame;

        TransactionIdText.Text = $"Id: {transaction.Id}";
        TransactionWalletIdText.Text = $"Гаманець: {transaction.WalletId}";
        TransactionDateText.Text = $"Дата: {transaction.Date:yyyy-MM-dd HH:mm}";
        TransactionCategoryText.Text = $"Категорія: {transaction.Category}";
        TransactionAmountText.Text = $"{transaction.Amount:N2}";
        TransactionAmountText.Foreground = transaction.IsExpense
            ? new SolidColorBrush(Color.FromRgb(200, 50, 50))
            : new SolidColorBrush(Color.FromRgb(40, 150, 40));
        TransactionTypeText.Text = transaction.IsExpense ? "Тип: Витрата" : "Тип: Дохід";
        TransactionDescriptionText.Text = $"Опис: {transaction.Description}";
    }

    private void BackButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        _frame.GoBack();
    }
}
