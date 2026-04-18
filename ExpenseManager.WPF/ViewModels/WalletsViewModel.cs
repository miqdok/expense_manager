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

    [ObservableProperty]
    private IReadOnlyList<WalletListDto> _wallets = [];

    [ObservableProperty]
    private bool _isLoading;

    public WalletsViewModel(IWalletService walletService, INavigationService navigationService)
    {
        _walletService = walletService;
        _navigationService = navigationService;
        _ = LoadWalletsAsync();
    }

    [RelayCommand]
    private async Task LoadWalletsAsync()
    {
        IsLoading = true;
        try
        {
            Wallets = await _walletService.GetWalletListAsync();
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void SelectWallet(WalletListDto wallet)
    {
        _navigationService.NavigateTo<WalletDetailsViewModel>(wallet.Id);
    }
}
