using System;
using EnjoyLearn.Models.Interfaces;

namespace EnjoyLearn.Models.Tranings.Words
{
  public class TraningResultModel<TEntity> where TEntity : class, IEntity<Guid>
  {
    public Guid Id { get; set; }

    public int Points { get; set; }

    public int SpendTime { get; set; }

  }
}
