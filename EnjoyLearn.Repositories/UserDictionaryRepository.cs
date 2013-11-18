using EnjoyLearn.Data.Core;

namespace EnjoyLearn.Repositories
{
  using EnjoyLearn.Data.DBInteractions;
  using EnjoyLearn.Models;
  using EnjoyLearn.Repositories.Interfaces;

    public class UserDictionaryRepository : EntityRepositoryBase<UserDictionaryModel>, IUserDictionaryRepository
    {
      public UserDictionaryRepository(IDBFactory databaseFactory)
        : base(databaseFactory)
      {
      }   
    }
}
