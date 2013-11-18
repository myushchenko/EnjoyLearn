using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnjoyLearn.Models.Authorize;

namespace EnjoyLearn.Services.Interfaces
{
  public interface IUserService
  {
    UserModel GetUser(int id);

    UserModel GetUser(string name);

    UserProfileModel GetUserProfile(Guid id);

    UserProfileModel GetUserProfileByUser(int id);

    UserProfileModel GetUserProfileByUser(string name);

    void Save(UserProfileModel userProfile);
  }
}
