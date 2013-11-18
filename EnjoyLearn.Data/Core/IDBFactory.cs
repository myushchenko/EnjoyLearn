
namespace EnjoyLearn.Data.DBInteractions
{
  using System;

  public interface IDBFactory : IDisposable
  {
    EnjoyLearnContext Get();
  }
}
