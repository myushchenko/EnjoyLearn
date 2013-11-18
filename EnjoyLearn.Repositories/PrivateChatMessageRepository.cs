using EnjoyLearn.Data.Core;
using EnjoyLearn.Models.Chat;

namespace EnjoyLearn.Repositories
{
  using EnjoyLearn.Data.DBInteractions;
  using EnjoyLearn.Models;
  using EnjoyLearn.Repositories.Interfaces;

  public class PrivateChatMessageRepository : EntityRepositoryBase<PrivateChatMessageModel>, IPrivateChatMessageRepository
    {
    public PrivateChatMessageRepository(IDBFactory databaseFactory)
        : base(databaseFactory)
      {
      }   
    }
}
