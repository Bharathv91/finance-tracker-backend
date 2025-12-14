using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceTracker.Infrastructure.Repositories
{
    public class EfExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _db;
        public EfExpenseRepository(AppDbContext db) => _db = db;

        public async Task AddAsync(Expense expense) => await _db.Expenses.AddAsync(expense);

        public void Delete(Expense expense) => _db.Expenses.Remove(expense);

        public async Task<Expense?> GetByIdAsync(Guid id) => await _db.Expenses
            .Include(e => e.Category)
            .Include(e => e.Account)
            .FirstOrDefaultAsync(e => e.Id == id);

        public async Task<IEnumerable<Expense>> ListAsync(int page = 1, int pageSize = 20) =>
            await _db.Expenses
                .Include(e => e.Category)
                .Include(e => e.Account)
                .OrderByDescending(e => e.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        public void Update(Expense expense) => _db.Expenses.Update(expense);
    }
}
