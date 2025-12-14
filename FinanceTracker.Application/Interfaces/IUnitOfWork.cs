using System.Threading;
using System.Threading.Tasks;

namespace FinanceTracker.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}