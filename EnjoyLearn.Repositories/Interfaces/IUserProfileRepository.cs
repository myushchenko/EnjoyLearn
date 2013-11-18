namespace EnjoyLearn.Repositories.Interfaces
{
  using EnjoyLearn.Data.DBInteractions;
  using EnjoyLearn.Models.Authorize;

  public interface IUserProfileRepository : IEntityRepository<UserProfileModel>
  {
  }
}
