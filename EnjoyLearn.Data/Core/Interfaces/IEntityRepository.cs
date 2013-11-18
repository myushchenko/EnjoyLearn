namespace EnjoyLearn.Data.Core.Interfaces
{
  using EnjoyLearn.Models.Interfaces;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Linq.Expressions;
  using System.Threading;
  using System.Threading.Tasks;

  public interface IEntityRepository<TEntity> : IEntityRepository<TEntity, Guid>
          where TEntity : class, IEntity<Guid>
  {
  }

  public interface IEntityRepository<TEntity, TId> : IRepository<TEntity, TId>
          where TEntity : class, IEntity<TId>
  {
    Task<List<TEntity>> LoadAllAsync();
    Task<List<TEntity>> LoadAllAsync(CancellationToken cancellationToken);

    IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
    Task<List<TEntity>> LoadAllIncludingAsync(CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeProperties);
    Task<List<TEntity>> LoadAllIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties);

    Task<List<TEntity>> LoadByAsync(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> LoadByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    Task<TEntity> GetSingleAsync(TId id);
    Task<TEntity> GetSingleAsync(TId id, CancellationToken cancellationToken);

    void Add(TEntity entity);
    void AddGraph(TEntity entity);
    void Edit(TEntity entity);
    void Delete(TEntity entity);
    int Save();
    Task<int> SaveAsync();
    Task<int> SaveAsync(CancellationToken cancellationToken);
  }
}
