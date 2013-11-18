using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using EnjoyLearn.Data.Core.Interfaces;
using EnjoyLearn.Models.Authorize;
using EnjoyLearn.Models.Rating;
using EnjoyLearn.Services.Interfaces;
using NLog.Mvc;
using WebMatrix.WebData;

namespace EnjoyLearn.Web.Controllers.API
{
  public class UserProfilesController : BaseApiController
  {
    private readonly IUserService _userProfileRepository;
    private readonly IEntityRepository<LevelModel> _repositoryLevel;

    public UserProfilesController(ILogger logger, IUserService userProfileRepository, IEntityRepository<LevelModel> repositoryLevel)
      : base(logger)
    {
      this._userProfileRepository = userProfileRepository;
      this._repositoryLevel = repositoryLevel;
    }

    //GET
    public HttpResponseMessage Get()
    {
      //var userProfile = _userProfileRepository.FindBy(u => u.UserId == WebSecurity.CurrentUserId);
      var userProfile = _userProfileRepository.GetUserProfileByUser(WebSecurity.CurrentUserId);
      return Request.CreateResponse(HttpStatusCode.OK, userProfile);
    }

    // PUT
    public HttpResponseMessage Put(UserProfileModel model)
    {
      if (!ModelState.IsValid)
      {
        return Request.CreateErrorResponse(
            HttpStatusCode.BadRequest, ModelState);
      }



      var isNewLevel = false;

      var profile = _userProfileRepository.GetUserProfile(model.Id);
      if (profile == null)
      {
        return Request.CreateResponse(HttpStatusCode.NotFound);
      }

      profile.Points.Points = model.Points.Points;

      var nextLevel = _repositoryLevel.GetSingle(model.Points.NextLevelId);
      if (nextLevel != null && (nextLevel.Points - model.Points.Points) <= 0 && profile.Points.NextLevelId != profile.Points.LevelId)
      {
   
        var nextLevelId = Guid.Empty;
        var nextLevelType = nextLevel.Type + 1;
        IQueryable<LevelModel> nextLevelList = _repositoryLevel.GetAll();
        nextLevel = nextLevelList.FirstOrDefault(t => t.Type == nextLevelType);
        if (nextLevel != null)
        {
          nextLevelId = nextLevel.Id;
        }

        profile.Points.LevelId = model.Points.NextLevelId;
        profile.Points.NextLevelId = nextLevelId;
        isNewLevel = true;
      }
 

      if (profile.Points.NextLevelId != profile.Points.LevelId)
      {
        _userProfileRepository.Save(profile);  
      }

      var result = new { NextLevelPoints = nextLevel.Points, IsNewLevel = isNewLevel, NewLevelName = profile.Points.Level.Name };

      return Request.CreateResponse(HttpStatusCode.OK, result);
    }
  }
}
