using ExpenseManager.Services.Dto;

namespace ExpenseManager.Services;

public interface ITransactionService
{
    IReadOnlyList<TransactionListDto> GetTransactionList(Guid walletId);
    TransactionDetailsDto? GetTransactionDetails(Guid id);
}
