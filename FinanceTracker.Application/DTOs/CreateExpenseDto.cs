using System;

namespace FinanceTracker.Application.DTOs
{
    public record CreateExpenseDto(string Title, decimal Amount, DateTime Date, Guid? CategoryId, Guid? AccountId, string? Notes);
}
