using System.Windows;
using ExpenseManager.WPF.Pages;

namespace ExpenseManager.WPF;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainFrame.Navigate(new WalletsPage(MainFrame));
    }
}
