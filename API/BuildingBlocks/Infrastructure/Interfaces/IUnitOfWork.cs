using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Interfaces
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        Task<int> CommitAsync();
    }
}
