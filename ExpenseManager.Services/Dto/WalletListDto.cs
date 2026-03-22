using ExpenseManager.Repositories.Enums;

namespace ExpenseManager.Services.Dto;

public class WalletListDto
{
    public Guid Id { get; }
    public string Name { get; }
    public Currency Currency { get; }

    public WalletListDto(Guid id, string name, Currency currency)
    {
        Id = id;
        Name = name;
        Currency = currency;
    }
}
