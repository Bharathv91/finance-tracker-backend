using System;

namespace FinanceTracker.Application.DTOs
{
    public record UpdateExpenseDto(Guid Id, string Title, decimal Amount, DateTime Date, Guid? CategoryId, Guid? AccountId, string? Notes);
}
