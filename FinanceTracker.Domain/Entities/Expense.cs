using System;

namespace FinanceTracker.Domain.Entities
{
    public class Expense : BaseEntity
    {
        public string Title { get; private set; } = null!;
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public Guid? CategoryId { get; private set; }
        public Category? Category { get; private set; }
        public Guid? AccountId { get; private set; }
        public Account? Account { get; private set; }
        public string? Notes { get; private set; }

        protected Expense() { }

        public Expense(string title, decimal amount, DateTime date, Guid? categoryId = null, Guid? accountId = null, string? notes = null)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title required", nameof(title));
            if (amount <= 0) throw new ArgumentException("Amount must be positive", nameof(amount));

            Title = title;
            Amount = amount;
            Date = date;
            CategoryId = categoryId;
            AccountId = accountId;
            Notes = notes;
        }

        public void Update(string title, decimal amount, DateTime date, Guid? categoryId = null, Guid? accountId = null, string? notes = null)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title required", nameof(title));
            if (amount <= 0) throw new ArgumentException("Amount must be positive", nameof(amount));

            Title = title;
            Amount = amount;
            Date = date;
            CategoryId = categoryId;
            AccountId = accountId;
            Notes = notes;
            SetModified();
        }
    }
}
