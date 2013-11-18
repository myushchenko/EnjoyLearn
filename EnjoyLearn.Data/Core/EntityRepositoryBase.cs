using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EnjoyLearn.Data.DBInteractions;

namespace EnjoyLearn.Data.Core
{
  public abstract class EntityRepositoryBase<TEntity> where TEntity : class
  {
    private EnjoyLearnContext _dataContext;
    private readonly IDbSet<TEntity> _dbset;

    protected EntityRepositoryBase(IDBFactory databaseFactory)
    {
      DatabaseFactory = databaseFactory;
      _dbset = DataContext.Set<TEntity>();
    }

    protected IDBFactory DatabaseFactory
    {
      get;
      private set;
    }

    protected EnjoyLearnContext DataContext
    {
      get { return _dataContext ?? (_dataContext = DatabaseFactory.Get()); }
    }

    public void Commit()
    {
      DataContext.Commit();
    }

    public virtual void Add(TEntity entity)
    {
      _dbset.Add(entity);
      Commit();
    }

    public int Save()
    {
      return _dataContext.SaveChanges();
    }

    public virtual void Update(TEntity entity)
    {
      _dataContext.Entry(entity).State = EntityState.Modified;
      Commit();
    }

    public virtual void Delete(TEntity entity)
    {
      _dbset.Remove(entity);
      Commit();
    }

    public void Delete(Func<TEntity, Boolean> where)
    {
      IEnumerable<TEntity> objects = _dbset.Where<TEntity>(where).AsEnumerable();
      foreach (TEntity obj in objects)
        _dbset.Remove(obj);
    }

    public virtual TEntity GetById(Guid id)
    {
      return _dbset.Find(id);
    }

    public virtual IEnumerable<TEntity> GetAll()
    {
      return _dbset.ToList();
    }

    public virtual IEnumerable<TEntity> GetMany(Func<TEntity, bool> where)
    {
      return _dbset.Where(where).ToList();
    }

    public TEntity Get(Func<TEntity, Boolean> where)
    {
      return _dbset.Where(where).FirstOrDefault<TEntity>();
    }
  }
}
