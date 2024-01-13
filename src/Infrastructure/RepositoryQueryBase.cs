using Contracts.Domains;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure
{
    public class RepositoryQueryBase<T, K, TContext> : RepositoryMultiKeyQueryBase<T, TContext>, IRepositoryQueryBase<T, K, TContext>
         where T : EntityBase<K>
         where TContext : DbContext
    {
        private readonly TContext _dbContext;
        public RepositoryQueryBase(TContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<T?> GetByIdAsync(K id) =>
            await FindByCondition(x => x.Id.Equals(id))
            .FirstOrDefaultAsync();

        public async Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties) =>
            await FindByCondition(x => x.Equals(id), trackChanges: false, includeProperties)
            .FirstOrDefaultAsync();

    }
}
