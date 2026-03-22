using ExpenseManager.Repositories.Enums;

namespace ExpenseManager.Services.Dto;

public class WalletDetailsDto
{
    public Guid Id { get; }
    public string Name { get; }
    public Currency Currency { get; }
    public decimal TotalAmount { get; }

    public WalletDetailsDto(Guid id, string name, Currency currency, decimal totalAmount)
    {
        Id = id;
        Name = name;
        Currency = currency;
        TotalAmount = totalAmount;
    }
}
