using System;
using ExpenseManager.Storage.Enums;

namespace ExpenseManager.UI.Models
{
    public class TransactionModel
    {
        public Guid Id { get; }
        public Guid WalletId { get; }
        public decimal Amount { get; set; }
        public TransactionCategory Category { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        // negative = expense, positive/zero = income
        public bool IsExpense => Amount < 0;

        public TransactionModel(Guid id, Guid walletId, decimal amount, TransactionCategory category, string description, DateTime date)
        {
            Id = id;
            WalletId = walletId;
            Amount = amount;
            Category = category;
            Description = description;
            Date = date;
        }

        public void Update(decimal amount, TransactionCategory category, string description, DateTime date)
        {
            Amount = amount;
            Category = category;
            Description = description;
            Date = date;
        }

        public string ToListItem()
        {
            return $"{Date:yyyy-MM-dd HH:mm} | {Category} | {Amount}";
        }

        public string ToDetailsString()
        {
            var direction = IsExpense ? "Expense" : "Income";
            return $"Id: {Id}\nWallet: {WalletId}\nDate: {Date:yyyy-MM-dd HH:mm}\nCategory: {Category}\nAmount: {Amount}\nType: {direction}\nNote: {Description}";
        }
    }
}
