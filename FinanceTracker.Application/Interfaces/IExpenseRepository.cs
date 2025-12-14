using FinanceTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceTracker.Application.Interfaces
{
    public interface IExpenseRepository
    {
        Task<Expense?> GetByIdAsync(Guid id);
        Task<IEnumerable<Expense>> ListAsync(int page = 1, int pageSize = 20);
        Task AddAsync(Expense expense);
        void Update(Expense expense);
        void Delete(Expense expense);
    }
}