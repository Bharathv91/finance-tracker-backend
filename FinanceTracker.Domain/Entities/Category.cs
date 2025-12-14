using System;

namespace FinanceTracker.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; } = null!;

        protected Category() { }

        public Category(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException(nameof(name));
            Name = name;
        }
    }
}
