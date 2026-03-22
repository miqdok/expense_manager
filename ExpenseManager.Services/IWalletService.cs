using ExpenseManager.Services.Dto;

namespace ExpenseManager.Services;

public interface IWalletService
{
    IReadOnlyList<WalletListDto> GetWalletList();
    WalletDetailsDto? GetWalletDetails(Guid id);
}
