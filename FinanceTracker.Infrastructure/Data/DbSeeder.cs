using FinanceTracker.Domain.Entities;
using FinanceTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FinanceTracker.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            if (await db.Categories.AnyAsync()) return;

            var groceries = new Category("Groceries");
            var transport = new Category("Transport");
            var utilities = new Category("Utilities");

            await db.Categories.AddRangeAsync(groceries, transport, utilities);

            var cash = new Account("Cash Wallet", 5000m);
            var bank = new Account("Bank - Checking", 20000m);
            await db.Accounts.AddRangeAsync(cash, bank);

            // Save early to ensure GUIDs are set
            await db.SaveChangesAsync();

            var e1 = new Expense("Buy rice & veggies", 450.75m, DateTime.UtcNow.AddDays(-3), groceries.Id, cash.Id, "weekly groceries");
            var e2 = new Expense("Bus pass", 250m, DateTime.UtcNow.AddDays(-2), transport.Id, cash.Id, null);
            var e3 = new Expense("Electricity bill", 1200m, DateTime.UtcNow.AddDays(-10), utilities.Id, bank.Id, "Oct bill");

            await db.Expenses.AddRangeAsync(e1, e2, e3);
            await db.SaveChangesAsync();
        }
    }
}
