using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.Repository
{
    public interface IRepository<TEntity>
    {
        #region Get

        IQueryable<TEntity> GetQuery();

        IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> expression);

        IQueryable<TEntity> GetQueryNoTracking();

        IQueryable<TEntity> GetQueryNoTracking(Expression<Func<TEntity, bool>> expression);

        TEntity Single(Expression<Func<TEntity, bool>> criteria);

        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken = default);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> criteria);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken = default);

        #endregion Get

        #region Find

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> criteria);

        TEntity FindOne(Expression<Func<TEntity, bool>> criteria);

        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken = default);

        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, object>>[] includeExpressions, CancellationToken cancellationToken = default);

        #endregion Find

        #region Count

        int Count();

        int Count(Expression<Func<TEntity, bool>> criteria);

        Task<int> CountAsync(CancellationToken cancellationToken = default);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken = default);

        Task<bool> GetAnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

        #endregion Count

        #region Update

        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);

        // New async method for updating
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        #endregion Update

        #region Add

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        // New async methods for adding
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        #endregion Add

        #region Remove

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        // New async methods for removing
        Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        #endregion Remove

        #region Sql

        IQueryable<TEntity> GetDataFromQuery(string query);

        #endregion Sql

        void Reload(TEntity entity);
    }
}
