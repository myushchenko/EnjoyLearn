using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnjoyLearn.Data.Core.Interfaces;
using EnjoyLearn.Models.Authorize;
using EnjoyLearn.Services.Interfaces;

namespace EnjoyLearn.Services
{
  public class UserService : IUserService
  {
     private readonly IEntityRepository<UserProfileModel> _userProfileRepository;

     public UserService(IEntityRepository<UserProfileModel> userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

    public UserModel GetUser(int id)
    {
      throw new NotImplementedException();
    }

    public UserModel GetUser(string name)
    {
      throw new NotImplementedException();
    }

    public UserProfileModel GetUserProfile(Guid id)
    {
     return _userProfileRepository.GetSingle(id);
    }

    public UserProfileModel GetUserProfileByUser(int id)
    {
      var userProfile = _userProfileRepository.GetAll().FirstOrDefault(u => u.UserId == id);
      return userProfile;
    }

    public UserProfileModel GetUserProfileByUser(string name)
    {
      throw new NotImplementedException();
    }

    public void Save(UserProfileModel userProfile)
    {
      _userProfileRepository.Edit(userProfile);
      _userProfileRepository.Save();
    }

    public UserProfileModel GetUserProfile(int id)
    {
      throw new NotImplementedException();
    }

    public UserProfileModel GetUserProfile(string name)
    {
      throw new NotImplementedException();
    }
  }
}
