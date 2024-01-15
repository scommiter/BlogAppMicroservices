using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IRepositoryQueryBase<T, K, TContext> : IRepositoryQueryBase<T, K>
        where T : EntityBase<K>
        where TContext : DbContext
    {
    }

    public interface IRepositoryMultiKeyBase<T, TContext> : IRepositoryMultiKeyQueryBaseAsync<T>
        where T : class
        where TContext : DbContext
    {
    }

    public interface IRepositoryBase<T, K, TContext> : IRepositoryBaseAsync<T, K>
        where T : EntityBase<K>
        where TContext : DbContext
    {
    }

    public interface IRepositoryMultiKey<T, TContext> : IRepositoryMultiKeyBaseAsync<T>
    where T : class
    where TContext : DbContext
    {
    }

    public interface IRepositoryQueryBase<T, K> : IRepositoryMultiKeyQueryBase<T>
        where T : EntityBase<K>
    {
        Task<T?> GetByIdAsync(K id);
        Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties);
    }

    public interface IRepositoryBaseAsync<T, K> : IRepositoryQueryBase<T, K>, IRepositoryMultiKeyBase<T>
        where T : EntityBase<K>
    {
        Task<K> CreateAsync(T entity);
        Task<IList<K>> CreateListAsync(IEnumerable<T> entities);
    }

    #region For classes with many keys
    public interface IRepositoryMultiKeyBaseAsync<T> : IRepositoryMultiKeyBase<T> where T : class
    {
        Task CreateAsync(T entity);
        Task CreateListAsync(IEnumerable<T> entities);
    }

    public interface IRepositoryMultiKeyQueryBaseAsync<T> : IRepositoryMultiKeyQueryBase<T>
        where T : class
    {
    }

    public interface IRepositoryMultiKeyBase<T> where T : class
    {
        void Create(T entity);
        void Update(T entity);
        void UpdateList(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteList(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateListAsync(IEnumerable<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteListAsync(IEnumerable<T> entities);
        Task<int> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task EndTransactionAsync();
        Task RollbackTransactionAsync();
    }

    public interface IRepositoryMultiKeyQueryBase<T>
        where T : class
    {
        IQueryable<T> FindAll(bool trackChanges = false);
        IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);
    }
    #endregion
}
