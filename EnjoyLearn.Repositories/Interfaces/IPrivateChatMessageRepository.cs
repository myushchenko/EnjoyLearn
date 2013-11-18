namespace EnjoyLearn.Repositories.Interfaces
{
  using EnjoyLearn.Data.DBInteractions;
  using EnjoyLearn.Models;
  using EnjoyLearn.Models.Chat;

  public interface IPrivateChatMessageRepository : IEntityRepository<PrivateChatMessageModel> { }

}
