using ExpenseManager.Repositories;
using ExpenseManager.Services.Dto;

namespace ExpenseManager.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public IReadOnlyList<TransactionListDto> GetTransactionList(Guid walletId)
    {
        return _transactionRepository.GetByWalletId(walletId)
            .Select(t => new TransactionListDto(t.Id, t.Date, t.Category, t.Amount, t.Description))
            .ToList();
    }

    public TransactionDetailsDto? GetTransactionDetails(Guid id)
    {
        var transaction = _transactionRepository.GetById(id);
        if (transaction == null)
            return null;

        return new TransactionDetailsDto(
            transaction.Id,
            transaction.WalletId,
            transaction.Date,
            transaction.Category,
            transaction.Amount,
            transaction.Description);
    }
}
