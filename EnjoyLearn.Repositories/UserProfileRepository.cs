using EnjoyLearn.Data.Core;

namespace EnjoyLearn.Repositories
{
  using EnjoyLearn.Data.DBInteractions;
  using EnjoyLearn.Models.Authorize;
  using EnjoyLearn.Repositories.Interfaces;

    public class UserProfileRepository : EntityRepositoryBase<UserProfileModel>, IUserProfileRepository
    {
      public UserProfileRepository(IDBFactory databaseFactory)
            : base(databaseFactory)
        {
        }
      
    }
}
