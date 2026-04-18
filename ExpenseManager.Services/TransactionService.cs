using ExpenseManager.Repositories;
using ExpenseManager.Repositories.Enums;
using ExpenseManager.Repositories.Models;
using ExpenseManager.Services.Dto;

namespace ExpenseManager.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<IReadOnlyList<TransactionListDto>> GetTransactionListAsync(Guid walletId)
    {
        var transactions = await _transactionRepository.GetByWalletIdAsync(walletId);
        return transactions
            .Select(t => new TransactionListDto(t.Id, t.Date, t.Category, t.Amount, t.Description))
            .ToList();
    }

    public async Task<TransactionDetailsDto?> GetTransactionDetailsAsync(Guid id)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id);
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

    public async Task AddTransactionAsync(Guid walletId, decimal amount, TransactionCategory category, string description, DateTime date)
    {
        var transaction = new TransactionEntity(walletId, amount, category, description, date);
        await _transactionRepository.AddAsync(transaction);
    }

    public async Task UpdateTransactionAsync(Guid id, decimal amount, TransactionCategory category, string description, DateTime date)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id);
        if (transaction == null)
            return;

        transaction.Amount = amount;
        transaction.Category = category;
        transaction.Description = description;
        transaction.Date = date;
        await _transactionRepository.UpdateAsync(transaction);
    }

    public async Task DeleteTransactionAsync(Guid id)
    {
        await _transactionRepository.DeleteAsync(id);
    }
}
