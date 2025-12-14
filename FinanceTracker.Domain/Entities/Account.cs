using System;

namespace FinanceTracker.Domain.Entities
{
    public class Account : BaseEntity
    {
        public string Name { get; private set; } = null!;
        public decimal Balance { get; private set; }

        protected Account() { }

        public Account(string name, decimal balance = 0)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException(nameof(name));
            Name = name;
            Balance = balance;
        }

        public void AdjustBalance(decimal amount)
        {
            Balance += amount;
            SetModified();
        }
    }
}
