using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using ExpenseManager.WPF.ViewModels;
using ExpenseManager.WPF.Views;

namespace ExpenseManager.WPF.Services;

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private Frame? _frame;

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void SetFrame(Frame frame)
    {
        _frame = frame;
    }

    public void NavigateTo<TViewModel>(object? parameter = null) where TViewModel : class
    {
        var viewModel = _serviceProvider.GetRequiredService<TViewModel>();

        if (viewModel is IParameterReceiver receiver && parameter != null)
        {
            receiver.ReceiveParameter(parameter);
        }

        var page = CreatePage(viewModel);
        _frame?.Navigate(page);
    }

    public void GoBack()
    {
        if (_frame?.CanGoBack == true)
            _frame.GoBack();
    }

    private static Page CreatePage(object viewModel)
    {
        Page page = viewModel switch
        {
            WalletsViewModel => new WalletsPage(),
            WalletDetailsViewModel => new WalletDetailsPage(),
            TransactionDetailsViewModel => new TransactionDetailsPage(),
            _ => throw new ArgumentException($"no view for {viewModel.GetType().Name}")
        };

        page.DataContext = viewModel;
        return page;
    }
}
