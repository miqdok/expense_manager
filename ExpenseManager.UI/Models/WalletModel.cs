using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseManager.Storage.Enums;

namespace ExpenseManager.UI.Models
{
    public class WalletModel
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public Currency Currency { get; set; }
        // shows whether transactions were fetched at least once
        public bool TransactionsLoaded { get; private set; }
        public IReadOnlyList<TransactionModel> Transactions { get; private set; }
        public decimal TotalAmount => Transactions.Sum(t => t.Amount);

        public WalletModel(Guid id, string name, Currency currency)
        {
            Id = id;
            Name = name;
            Currency = currency;
            Transactions = Array.Empty<TransactionModel>();
        }

        public void Update(string name, Currency currency)
        {
            Name = name;
            Currency = currency;
        }

        public void SetTransactions(IEnumerable<TransactionModel> transactions)
        {
            Transactions = transactions.ToList();
            TransactionsLoaded = true;
        }

        public string ToListItem()
        {
            return $"{Name} [{Currency}]";
        }

        public string ToDetailsString()
        {
            return $"Id: {Id}\nName: {Name}\nCurrency: {Currency}\nTotal: {TotalAmount}";
        }
    }
}
