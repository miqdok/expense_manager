using System;
using ExpenseManager.Storage.Enums;

namespace ExpenseManager.Storage.Models
{
    public class TransactionEntity
    {
        public Guid Id { get; }
        public Guid WalletId { get; }
        public decimal Amount { get; set; }
        public TransactionCategory Category { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public TransactionEntity(Guid id, Guid walletId, decimal amount, TransactionCategory category, string description, DateTime date)
        {
            Id = id;
            WalletId = walletId;
            Amount = amount;
            Category = category;
            Description = description;
            Date = date;
        }

        public TransactionEntity(Guid walletId, decimal amount, TransactionCategory category, string description, DateTime date)
            : this(Guid.NewGuid(), walletId, amount, category, description, date)
        {
        }
    }
}
