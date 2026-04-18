using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.Repositories.Enums;
using ExpenseManager.Services;
using ExpenseManager.WPF.Services;

namespace ExpenseManager.WPF.ViewModels;

public partial class WalletEditViewModel : ObservableObject, IParameterReceiver
{
    private readonly IWalletService _walletService;
    private readonly INavigationService _navigationService;
    private Guid? _walletId;

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private Currency _selectedCurrency;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotLoading))]
    private bool _isLoading;

    public bool IsNotLoading => !IsLoading;

    [ObservableProperty]
    private bool _isEditMode;

    [ObservableProperty]
    private string _pageTitle = "Новий гаманець";

    public IReadOnlyList<Currency> Currencies { get; } = Enum.GetValues<Currency>();

    public WalletEditViewModel(IWalletService walletService, INavigationService navigationService)
    {
        _walletService = walletService;
        _navigationService = navigationService;
    }

    public async Task ReceiveParameterAsync(object parameter)
    {
        if (parameter is Guid walletId)
        {
            _walletId = walletId;
            IsEditMode = true;
            PageTitle = "Редагувати гаманець";

            IsLoading = true;
            try
            {
                var wallet = await _walletService.GetWalletDetailsAsync(walletId);
                if (wallet != null)
                {
                    Name = wallet.Name;
                    SelectedCurrency = wallet.Currency;
                }
            }
            finally
            {
                IsLoading = false;
            }
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Name))
            return;

        IsLoading = true;
        try
        {
            if (IsEditMode && _walletId.HasValue)
                await _walletService.UpdateWalletAsync(_walletId.Value, Name.Trim(), SelectedCurrency);
            else
                await _walletService.AddWalletAsync(Name.Trim(), SelectedCurrency);

            _navigationService.GoBack();
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        _navigationService.GoBack();
    }
}
