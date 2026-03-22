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

    public TransactionDetailsViewModel(ITransactionService transactionService, INavigationService navigationService)
    {
        _transactionService = transactionService;
        _navigationService = navigationService;
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid transactionId)
        {
            Transaction = _transactionService.GetTransactionDetails(transactionId);
        }
    }

    [RelayCommand]
    private void GoBack()
    {
        _navigationService.GoBack();
    }
}
