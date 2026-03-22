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

    public WalletsViewModel(IWalletService walletService, INavigationService navigationService)
    {
        _walletService = walletService;
        _navigationService = navigationService;
        Wallets = _walletService.GetWalletList();
    }

    [RelayCommand]
    private void SelectWallet(WalletListDto wallet)
    {
        _navigationService.NavigateTo<WalletDetailsViewModel>(wallet.Id);
    }
}
