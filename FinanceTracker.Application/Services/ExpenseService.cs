using FinanceTracker.Application.DTOs;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceTracker.Application.Services
{
    public class ExpenseService
    {
        private readonly IExpenseRepository _repo;
        private readonly IUnitOfWork _uow;

        public ExpenseService(IExpenseRepository repo, IUnitOfWork uow)
        {
            _repo = repo;
            _uow = uow;
        }

        public async Task<ExpenseDto> CreateAsync(CreateExpenseDto dto)
        {
            var expense = new Expense(dto.Title, dto.Amount, dto.Date, dto.CategoryId, dto.AccountId, dto.Notes);
            await _repo.AddAsync(expense);
            await _uow.SaveChangesAsync();
            return new ExpenseDto(expense.Id, expense.Title, expense.Amount, expense.Date, expense.CategoryId, expense.AccountId, expense.Notes);
        }

        public async Task<ExpenseDto?> GetAsync(Guid id)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null) return null;
            return new ExpenseDto(e.Id, e.Title, e.Amount, e.Date, e.CategoryId, e.AccountId, e.Notes);
        }

        public async Task<IEnumerable<ExpenseDto>> ListAsync(int page = 1, int pageSize = 20)
        {
            var items = await _repo.ListAsync(page, pageSize);
            return items.Select(e => new ExpenseDto(e.Id, e.Title, e.Amount, e.Date, e.CategoryId, e.AccountId, e.Notes));
        }

        public async Task<bool> UpdateAsync(UpdateExpenseDto dto)
        {
            var existing = await _repo.GetByIdAsync(dto.Id);
            if (existing == null) return false;
            existing.Update(dto.Title, dto.Amount, dto.Date, dto.CategoryId, dto.AccountId, dto.Notes);
            _repo.Update(existing);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;
            existing.MarkDeleted();
            _repo.Update(existing);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
