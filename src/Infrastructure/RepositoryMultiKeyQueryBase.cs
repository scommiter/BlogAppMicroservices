using Contracts.Domains;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure
{
    public class RepositoryMultiKeyQueryBase<T, TContext> : IRepositoryMultiKeyBase<T, TContext>
         where T : class
         where TContext : DbContext
    {
        private readonly TContext _dbContext;
        public RepositoryMultiKeyQueryBase(TContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public IQueryable<T> FindAll(bool trackChanges = false) =>
                     !trackChanges ? _dbContext.Set<T>().AsNoTracking()
                                   : _dbContext.Set<T>();

        public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindAll(trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
            return items;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
            !trackChanges ? _dbContext.Set<T>().Where(expression).AsNoTracking()
                          : _dbContext.Set<T>().Where(expression);

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindByCondition(expression, trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
            return items;
        }
    }
}
