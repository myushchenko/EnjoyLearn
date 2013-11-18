using EnjoyLearn.Data.Core;
using EnjoyLearn.Models.Chat;

namespace EnjoyLearn.Repositories
{
  using EnjoyLearn.Data.DBInteractions;
  using EnjoyLearn.Models;
  using EnjoyLearn.Repositories.Interfaces;

  public class UserChatConnectionRepository : EntityRepositoryBase<UserChatConnectionModel>, IUserChatConnectionRepository
    {
      public UserChatConnectionRepository(IDBFactory databaseFactory)
        : base(databaseFactory)
      {
      }   
    }
}
