using ExpenseManager.Repositories.Enums;

namespace ExpenseManager.Services.Dto;

public class TransactionListDto
{
    public Guid Id { get; }
    public DateTime Date { get; }
    public TransactionCategory Category { get; }
    public decimal Amount { get; }
    public string Description { get; }

    public TransactionListDto(Guid id, DateTime date, TransactionCategory category, decimal amount, string description)
    {
        Id = id;
        Date = date;
        Category = category;
        Amount = amount;
        Description = description;
    }
}
