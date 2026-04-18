using System.Windows;
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
    private Guid _transactionId;

    [ObservableProperty]
    private TransactionDetailsDto? _transaction;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotLoading))]
    private bool _isLoading;

    public bool IsNotLoading => !IsLoading;

    public TransactionDetailsViewModel(ITransactionService transactionService, INavigationService navigationService)
    {
        _transactionService = transactionService;
        _navigationService = navigationService;
    }

    public async Task ReceiveParameterAsync(object parameter)
    {
        if (parameter is Guid transactionId)
        {
            _transactionId = transactionId;
            await LoadTransactionAsync();
        }
    }

    [RelayCommand]
    private async Task LoadTransactionAsync()
    {
        IsLoading = true;
        try
        {
            Transaction = await _transactionService.GetTransactionDetailsAsync(_transactionId);
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void EditTransaction()
    {
        if (Transaction != null)
            _navigationService.NavigateTo<TransactionEditViewModel>((Transaction.WalletId, _transactionId));
    }

    [RelayCommand]
    private async Task DeleteTransactionAsync()
    {
        var result = MessageBox.Show(
            "Видалити цю транзакцію?",
            "Підтвердження",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result != MessageBoxResult.Yes)
            return;

        IsLoading = true;
        try
        {
            await _transactionService.DeleteTransactionAsync(_transactionId);
            _navigationService.GoBack();
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void GoBack()
    {
        _navigationService.GoBack();
    }
}
