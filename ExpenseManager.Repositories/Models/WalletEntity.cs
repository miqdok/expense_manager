using ExpenseManager.Repositories.Enums;

namespace ExpenseManager.Repositories.Models
{
    public class WalletEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; set; } = string.Empty;
        public Currency Currency { get; set; }
        public List<TransactionEntity> Transactions { get; set; } = [];

        private WalletEntity() { }

        public WalletEntity(Guid id, string name, Currency currency)
        {
            Id = id;
            Name = name;
            Currency = currency;
        }

        public WalletEntity(string name, Currency currency)
            : this(Guid.NewGuid(), name, currency)
        {
        }
    }
}
