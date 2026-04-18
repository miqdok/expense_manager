using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.Services;
using ExpenseManager.Services.Dto;
using ExpenseManager.WPF.Services;

namespace ExpenseManager.WPF.ViewModels;

public partial class TransactionDetailsViewModel : ObservableObject, IParameterReceiver
{
    private readonly ITransactionService _transactionService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private TransactionDetailsDto? _transaction;

    [ObservableProperty]
    private bool _isLoading;

    public TransactionDetailsViewModel(ITransactionService transactionService, INavigationService navigationService)
    {
        _transactionService = transactionService;
        _navigationService = navigationService;
    }

    public async Task ReceiveParameterAsync(object parameter)
    {
        if (parameter is Guid transactionId)
        {
            IsLoading = true;
            try
            {
                Transaction = await _transactionService.GetTransactionDetailsAsync(transactionId);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }

    [RelayCommand]
    private void GoBack()
    {
        _navigationService.GoBack();
    }
}
