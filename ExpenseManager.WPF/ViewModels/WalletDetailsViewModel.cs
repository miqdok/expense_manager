using System.Windows;
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
    private Guid _walletId;
    private IReadOnlyList<TransactionListDto> _allTransactions = [];

    [ObservableProperty]
    private WalletDetailsDto? _wallet;

    [ObservableProperty]
    private IReadOnlyList<TransactionListDto> _transactions = [];

    [ObservableProperty]
    private bool _hasTransactions;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotLoading))]
    private bool _isLoading;

    public bool IsNotLoading => !IsLoading;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private int _selectedSortIndex;

    public WalletDetailsViewModel(
        IWalletService walletService,
        ITransactionService transactionService,
        INavigationService navigationService)
    {
        _walletService = walletService;
        _transactionService = transactionService;
        _navigationService = navigationService;
    }

    partial void OnSearchTextChanged(string value) => ApplyFilter();
    partial void OnSelectedSortIndexChanged(int value) => ApplyFilter();

    public async Task ReceiveParameterAsync(object parameter)
    {
        if (parameter is Guid walletId)
        {
            _walletId = walletId;
            await LoadDataAsync();
        }
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        IsLoading = true;
        try
        {
            Wallet = await _walletService.GetWalletDetailsAsync(_walletId);
            _allTransactions = await _transactionService.GetTransactionListAsync(_walletId);
            ApplyFilter();
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void ApplyFilter()
    {
        var filtered = _allTransactions.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(SearchText))
            filtered = filtered.Where(t =>
                t.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                t.Category.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase));

        Transactions = SelectedSortIndex switch
        {
            1 => filtered.OrderBy(t => t.Amount).ToList(),
            2 => filtered.OrderBy(t => t.Category.ToString()).ToList(),
            _ => filtered.OrderByDescending(t => t.Date).ToList()
        };

        HasTransactions = Transactions.Count > 0;
    }

    [RelayCommand]
    private void SelectTransaction(TransactionListDto transaction)
    {
        _navigationService.NavigateTo<TransactionDetailsViewModel>(transaction.Id);
    }

    [RelayCommand]
    private void AddTransaction()
    {
        _navigationService.NavigateTo<TransactionEditViewModel>(_walletId);
    }

    [RelayCommand]
    private void EditWallet()
    {
        _navigationService.NavigateTo<WalletEditViewModel>(_walletId);
    }

    [RelayCommand]
    private async Task DeleteWalletAsync()
    {
        var result = MessageBox.Show(
            $"Видалити гаманець \"{Wallet?.Name}\" та всі його транзакції?",
            "Підтвердження",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result != MessageBoxResult.Yes)
            return;

        IsLoading = true;
        try
        {
            await _walletService.DeleteWalletAsync(_walletId);
            _navigationService.GoBack();
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task DeleteTransactionAsync(TransactionListDto transaction)
    {
        var result = MessageBox.Show(
            $"Видалити транзакцію \"{transaction.Description}\"?",
            "Підтвердження",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result != MessageBoxResult.Yes)
            return;

        IsLoading = true;
        try
        {
            await _transactionService.DeleteTransactionAsync(transaction.Id);
            await LoadDataAsync();
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
