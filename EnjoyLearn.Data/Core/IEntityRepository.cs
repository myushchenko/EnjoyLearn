namespace EnjoyLearn.Data.DBInteractions
{
  using System;
  using System.Collections.Generic;

  public interface IEntityRepository<TEntity> where TEntity : class
  {
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    void Delete(Func<TEntity, Boolean> predicate);
    TEntity GetById(Guid id);
    TEntity Get(Func<TEntity, Boolean> where);
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> GetMany(Func<TEntity, bool> where);

    int Save();

  }
}
