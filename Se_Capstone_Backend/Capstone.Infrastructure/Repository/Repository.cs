using Capstone.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable, IAsyncDisposable where TEntity : class
    {
        protected readonly SeCapstoneContext Context;
        private readonly Type _type;

        public Repository(SeCapstoneContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = Context.Set<TEntity>();
            _type = typeof(TEntity);
        }

        protected DbSet<TEntity> DbSet { get; }

        #region Update

        public void Update(TEntity entity)
        {
            if (entity != null)
            {
                Context.Update(entity);
            }
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities != null)
            {
                Context.UpdateRange(entities);
            }
        }

        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity != null)
            {
                Context.Update(entity);
            }
            return Context.SaveChangesAsync(cancellationToken);
        }

        public Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities != null)
            {
                Context.UpdateRange(entities);
            }
            return Context.SaveChangesAsync(cancellationToken);
        }

        #endregion Update

        #region Add

        public void Add(TEntity entity)
        {
            if (entity != null)
            {
                DbSet.Add(entity);
            }
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            if (entities != null)
            {
                DbSet.AddRange(entities);
            }
        }

        public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity != null)
            {
                DbSet.Add(entity);
            }
            return Context.SaveChangesAsync(cancellationToken);
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities != null)
            {
                DbSet.AddRange(entities);
            }
            return Context.SaveChangesAsync(cancellationToken);
        }

        #endregion Add

        #region Remove

        public void Remove(TEntity entity)
        {
            if (entity != null)
            {
                DbSet.Remove(entity);
            }
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            if (entities != null)
            {
                DbSet.RemoveRange(entities);
            }
        }

        public Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity != null)
            {
                DbSet.Remove(entity);
            }
            return Context.SaveChangesAsync(cancellationToken);
        }

        public Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities != null)
            {
                DbSet.RemoveRange(entities);
            }
            return Context.SaveChangesAsync(cancellationToken);
        }

        #endregion Remove

        #region Get

        public IQueryable<TEntity> GetQuery()
        {
            return DbSet;
        }

        public IQueryable<TEntity> GetQueryNoTracking()
        {
            return DbSet.AsNoTracking();
        }

        public IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> expression)
        {
            return Context.Set<TEntity>().Where(expression);
        }

        public IQueryable<TEntity> GetQueryNoTracking(Expression<Func<TEntity, bool>> expression)
        {
            return Context.Set<TEntity>().AsNoTracking().Where(expression);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> criteria)
        {
            return GetQuery().Single(criteria);
        }

        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return GetQuery().SingleAsync(criteria);
        }

        public TEntity? SingleOrDefault(Expression<Func<TEntity, bool>> criteria)
        {
            return GetQuery().SingleOrDefault(criteria);
        }

        public Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return GetQuery().SingleOrDefaultAsync(criteria);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> criteria)
        {
            return GetQuery().Where(criteria);
        }

        public TEntity? FindOne(Expression<Func<TEntity, bool>> criteria)
        {
            return GetQuery().Where(criteria).FirstOrDefault();
        }

        public Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> criteria)
        {
            return GetQuery().Where(criteria).FirstOrDefaultAsync();
        }

        public Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>>[] includeExpressions, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> includeQueryable = DbSet;
            foreach (Expression<Func<TEntity, object>>? includeExpression in includeExpressions)
            {
                includeQueryable = includeQueryable.Include(includeExpression);
            }

            return includeQueryable.FirstOrDefaultAsync(expression, cancellationToken);
        }

        public int Count()
        {
            return GetQuery().Count();
        }

        public Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return GetQuery().CountAsync(cancellationToken);
        }

        public int Count(Expression<Func<TEntity, bool>> criteria)
        {
            return GetQuery().Count(criteria);
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken = default)
        {
            return GetQuery().CountAsync(criteria, cancellationToken);
        }

        public async Task<bool> GetAnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await Context.Set<TEntity>().AnyAsync(expression, cancellationToken);
        }

        #endregion Get

        #region Sql

        public IQueryable<TEntity> GetDataFromQuery(string query)
        {
            return Context.Set<TEntity>().FromSqlRaw(query);
        }

        #endregion Sql

        public void Reload(TEntity entity)
        {
            if (entity != null)
            {
                Context.Entry(entity).Reload();
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await Context.DisposeAsync();
        }

        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken)
        {
            return GetQuery().SingleAsync(criteria, cancellationToken);
        }

        public Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken)
        {
            return GetQuery().SingleOrDefaultAsync(criteria, cancellationToken);
        }

        public Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken)
        {
            return GetQuery().Where(criteria).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
