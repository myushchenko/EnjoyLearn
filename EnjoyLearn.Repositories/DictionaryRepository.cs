using EnjoyLearn.Data.Core;

namespace EnjoyLearn.Repositories
{
  using EnjoyLearn.Data.DBInteractions;
  using EnjoyLearn.Models;
  using EnjoyLearn.Repositories.Interfaces;

    public class DictionaryRepository : EntityRepositoryBase<DictionaryModel>, IDictionaryRepository
    {
      public DictionaryRepository(IDBFactory databaseFactory)
        : base(databaseFactory)
      {
      }   
    }
}
