using EnjoyLearn.Models.Chat;

namespace EnjoyLearn.Repositories.Interfaces
{
  using EnjoyLearn.Models;
  using EnjoyLearn.Data.DBInteractions;

  public interface IUserChatConnectionRepository : IEntityRepository<UserChatConnectionModel> { }

}
