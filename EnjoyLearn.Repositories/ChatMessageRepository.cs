using EnjoyLearn.Data.Core;
using EnjoyLearn.Models.Chat;

namespace EnjoyLearn.Repositories
{
  using EnjoyLearn.Data.DBInteractions;
  using EnjoyLearn.Models;
  using EnjoyLearn.Repositories.Interfaces;

  public class ChatMessageRepository : EntityRepositoryBase<ChatMessageModel>, IChatMessageRepository
    {
      public ChatMessageRepository(IDBFactory databaseFactory)
        : base(databaseFactory)
      {
      }   
    }
}
