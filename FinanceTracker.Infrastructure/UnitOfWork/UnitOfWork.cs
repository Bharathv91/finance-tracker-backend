using FinanceTracker.Application.Interfaces;
using FinanceTracker.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace FinanceTracker.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db) => _db = db;
        public Task<int> SaveChangesAsync(CancellationToken ct = default) => _db.SaveChangesAsync(ct);
    }
}
