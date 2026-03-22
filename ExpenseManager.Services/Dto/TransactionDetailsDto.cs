using ExpenseManager.Repositories.Enums;

namespace ExpenseManager.Services.Dto;

public class TransactionDetailsDto
{
    public Guid Id { get; }
    public Guid WalletId { get; }
    public DateTime Date { get; }
    public TransactionCategory Category { get; }
    public decimal Amount { get; }
    public bool IsExpense { get; }
    public string Description { get; }

    public TransactionDetailsDto(Guid id, Guid walletId, DateTime date, TransactionCategory category, decimal amount, string description)
    {
        Id = id;
        WalletId = walletId;
        Date = date;
        Category = category;
        Amount = amount;
        IsExpense = amount < 0;
        Description = description;
    }
}
