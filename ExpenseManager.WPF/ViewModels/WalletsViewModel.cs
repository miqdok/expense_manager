using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.Services;
using ExpenseManager.Services.Dto;
using ExpenseManager.WPF.Services;

namespace ExpenseManager.WPF.ViewModels;

public partial class WalletsViewModel : ObservableObject
{
    private readonly IWalletService _walletService;
    private readonly INavigationService _navigationService;
    private IReadOnlyList<WalletListDto> _allWallets = [];

    [ObservableProperty]
    private IReadOnlyList<WalletListDto> _wallets = [];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotLoading))]
    private bool _isLoading;

    public bool IsNotLoading => !IsLoading;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private int _selectedSortIndex;

    public WalletsViewModel(IWalletService walletService, INavigationService navigationService)
    {
        _walletService = walletService;
        _navigationService = navigationService;
    }

    partial void OnSearchTextChanged(string value) => ApplyFilter();
    partial void OnSelectedSortIndexChanged(int value) => ApplyFilter();

    [RelayCommand]
    private async Task LoadWalletsAsync()
    {
        IsLoading = true;
        try
        {
            _allWallets = await _walletService.GetWalletListAsync();
            ApplyFilter();
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void ApplyFilter()
    {
        var filtered = _allWallets.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(SearchText))
            filtered = filtered.Where(w => w.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

        Wallets = SelectedSortIndex switch
        {
            1 => filtered.OrderBy(w => w.Currency.ToString()).ToList(),
            _ => filtered.OrderBy(w => w.Name).ToList()
        };
    }

    [RelayCommand]
    private void SelectWallet(WalletListDto wallet)
    {
        _navigationService.NavigateTo<WalletDetailsViewModel>(wallet.Id);
    }

    [RelayCommand]
    private void AddWallet()
    {
        _navigationService.NavigateTo<WalletEditViewModel>();
    }

    [RelayCommand]
    private async Task DeleteWalletAsync(WalletListDto wallet)
    {
        var result = MessageBox.Show(
            $"Видалити гаманець \"{wallet.Name}\" та всі його транзакції?",
            "Підтвердження",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result != MessageBoxResult.Yes)
            return;

        IsLoading = true;
        try
        {
            await _walletService.DeleteWalletAsync(wallet.Id);
            await LoadWalletsAsync();
        }
        finally
        {
            IsLoading = false;
        }
    }
}
