using ExpenseManager.Repositories.Enums;

namespace ExpenseManager.Repositories.Models
{
    public class TransactionEntity
    {
        public Guid Id { get; private set; }
        public Guid WalletId { get; private set; }
        public decimal Amount { get; set; }
        public TransactionCategory Category { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public WalletEntity? Wallet { get; set; }

        private TransactionEntity() { }

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
