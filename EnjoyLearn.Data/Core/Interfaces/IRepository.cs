using System;
using System.Linq;
using System.Linq.Expressions;
using EnjoyLearn.Models.Interfaces;

namespace EnjoyLearn.Data.Core.Interfaces
{
    public interface IRepository<TEntity, in TId>
            where TEntity : class, IEntity<TId>
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        TEntity GetSingle(TId id);
    }
}
