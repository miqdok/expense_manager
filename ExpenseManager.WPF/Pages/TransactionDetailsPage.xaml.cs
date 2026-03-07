using System.Windows.Controls;
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
        TransactionDateText.Text = $"Дата: {transaction.Date:yyyy-MM-dd HH:mm}";
        TransactionCategoryText.Text = $"Категорія: {transaction.Category}";
        TransactionAmountText.Text = $"{transaction.Amount:N2}";
        TransactionTypeText.Text = transaction.IsExpense ? "Тип: Витрата" : "Тип: Дохід";
        TransactionDescriptionText.Text = $"Опис: {transaction.Description}";
    }

    private void BackButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        _frame.GoBack();
    }
}
