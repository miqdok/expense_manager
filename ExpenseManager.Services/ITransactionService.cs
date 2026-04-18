using ExpenseManager.Repositories.Enums;
using ExpenseManager.Services.Dto;

namespace ExpenseManager.Services;

public interface ITransactionService
{
    Task<IReadOnlyList<TransactionListDto>> GetTransactionListAsync(Guid walletId);
    Task<TransactionDetailsDto?> GetTransactionDetailsAsync(Guid id);
    Task AddTransactionAsync(Guid walletId, decimal amount, TransactionCategory category, string description, DateTime date);
    Task UpdateTransactionAsync(Guid id, decimal amount, TransactionCategory category, string description, DateTime date);
    Task DeleteTransactionAsync(Guid id);
}
