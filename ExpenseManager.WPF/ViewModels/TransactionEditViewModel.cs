using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.Repositories.Enums;
using ExpenseManager.Services;
using ExpenseManager.WPF.Services;

namespace ExpenseManager.WPF.ViewModels;

public partial class TransactionEditViewModel : ObservableObject, IParameterReceiver
{
    private readonly ITransactionService _transactionService;
    private readonly INavigationService _navigationService;
    private Guid _walletId;
    private Guid? _transactionId;

    [ObservableProperty]
    private decimal _amount;

    [ObservableProperty]
    private TransactionCategory _selectedCategory;

    [ObservableProperty]
    private string _description = string.Empty;

    [ObservableProperty]
    private DateTime _date = DateTime.Now;

    [ObservableProperty]
    private bool _isExpense = true;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotLoading))]
    private bool _isLoading;

    public bool IsNotLoading => !IsLoading;

    [ObservableProperty]
    private bool _isEditMode;

    [ObservableProperty]
    private string _pageTitle = "Нова транзакція";

    public IReadOnlyList<TransactionCategory> Categories { get; } = Enum.GetValues<TransactionCategory>();

    public TransactionEditViewModel(ITransactionService transactionService, INavigationService navigationService)
    {
        _transactionService = transactionService;
        _navigationService = navigationService;
    }

    public async Task ReceiveParameterAsync(object parameter)
    {
        if (parameter is (Guid walletId, Guid transactionId))
        {
            _walletId = walletId;
            _transactionId = transactionId;
            IsEditMode = true;
            PageTitle = "Редагувати транзакцію";

            IsLoading = true;
            try
            {
                var tx = await _transactionService.GetTransactionDetailsAsync(transactionId);
                if (tx != null)
                {
                    Amount = Math.Abs(tx.Amount);
                    SelectedCategory = tx.Category;
                    Description = tx.Description;
                    Date = tx.Date;
                    IsExpense = tx.Amount < 0;
                }
            }
            finally
            {
                IsLoading = false;
            }
        }
        else if (parameter is Guid wId)
        {
            _walletId = wId;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Description))
            return;

        var finalAmount = IsExpense ? -Math.Abs(Amount) : Math.Abs(Amount);

        IsLoading = true;
        try
        {
            if (IsEditMode && _transactionId.HasValue)
                await _transactionService.UpdateTransactionAsync(_transactionId.Value, finalAmount, SelectedCategory, Description.Trim(), Date);
            else
                await _transactionService.AddTransactionAsync(_walletId, finalAmount, SelectedCategory, Description.Trim(), Date);

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
