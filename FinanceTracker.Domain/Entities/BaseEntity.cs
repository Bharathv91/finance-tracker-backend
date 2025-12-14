using System;

namespace FinanceTracker.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; protected set; }
        public bool IsDeleted { get; protected set; } = false;

        public void MarkDeleted() => IsDeleted = true;
        protected void SetModified() => ModifiedAt = DateTime.UtcNow;
    }
}
