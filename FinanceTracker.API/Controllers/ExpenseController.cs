using FinanceTracker.Application.DTOs;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Application.Services;
using FinanceTracker.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ExpenseService _service;
        private readonly AppDbContext _dbContext;

        public ExpensesController(ExpenseService service, AppDbContext dbContext)
        {
            _service = service;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get all available accounts for dropdown/selection
        /// </summary>
        [HttpGet("accounts")]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _dbContext.Accounts
                .Where(a => !a.IsDeleted)
                .Select(a => new { a.Id, a.Name, a.Balance })
                .ToListAsync();
            return Ok(accounts);
        }

        /// <summary>
        /// Get all available categories for dropdown/selection
        /// </summary>
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _dbContext.Categories
                .Where(c => !c.IsDeleted)
                .Select(c => new { c.Id, c.Name })
                .ToListAsync();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateExpenseDto dto)
        {
            // Validate AccountId if provided
            if (dto.AccountId.HasValue)
            {
                var accountExists = await _dbContext.Accounts
                    .AnyAsync(a => a.Id == dto.AccountId && !a.IsDeleted);
                if (!accountExists)
                    return BadRequest(new { error = $"Account with ID {dto.AccountId} not found" });
            }

            // Validate CategoryId if provided
            if (dto.CategoryId.HasValue)
            {
                var categoryExists = await _dbContext.Categories
                    .AnyAsync(c => c.Id == dto.CategoryId && !c.IsDeleted);
                if (!categoryExists)
                    return BadRequest(new { error = $"Category with ID {dto.CategoryId} not found" });
            }

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var item = await _service.GetAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var items = await _service.ListAsync(page, pageSize);
            return Ok(items);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateExpenseDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var ok = await _service.UpdateAsync(dto);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
