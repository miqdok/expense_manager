using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.Services;
using ExpenseManager.Services.Dto;
using ExpenseManager.WPF.Services;

namespace ExpenseManager.WPF.ViewModels;

public partial class WalletDetailsViewModel : ObservableObject, IParameterReceiver
{
    private readonly IWalletService _walletService;
    private readonly ITransactionService _transactionService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private WalletDetailsDto? _wallet;

    [ObservableProperty]
    private IReadOnlyList<TransactionListDto> _transactions = [];

    [ObservableProperty]
    private bool _hasTransactions;

    [ObservableProperty]
    private bool _isLoading;

    public WalletDetailsViewModel(
        IWalletService walletService,
        ITransactionService transactionService,
        INavigationService navigationService)
    {
        _walletService = walletService;
        _transactionService = transactionService;
        _navigationService = navigationService;
    }

    public async Task ReceiveParameterAsync(object parameter)
    {
        if (parameter is Guid walletId)
        {
            IsLoading = true;
            try
            {
                Wallet = await _walletService.GetWalletDetailsAsync(walletId);
                Transactions = await _transactionService.GetTransactionListAsync(walletId);
                HasTransactions = Transactions.Count > 0;
            }
            finally
            {
                IsLoading = false;
            }
        }
    }

    [RelayCommand]
    private void SelectTransaction(TransactionListDto transaction)
    {
        _navigationService.NavigateTo<TransactionDetailsViewModel>(transaction.Id);
    }

    [RelayCommand]
    private void GoBack()
    {
        _navigationService.GoBack();
    }
}
