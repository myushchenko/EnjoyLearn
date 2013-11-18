using System.Net;
using System.Net.Http;
using EnjoyLearn.Data.Core.Interfaces;
using EnjoyLearn.Models.Authorize;
using EnjoyLearn.Models.Rating;
using NLog.Mvc;
namespace EnjoyLearn.Web.Controllers.API
{
  public class RatingLevelsController : BaseApiController
  {
    private readonly IEntityRepository<LevelModel> _repository;

    public RatingLevelsController(ILogger logger, IEntityRepository<LevelModel> repository)
      : base(logger)
    {
      this._repository = repository;
    }

    //GET
    public HttpResponseMessage Get()
    {
      var levels = _repository.GetAll();
      return Request.CreateResponse(HttpStatusCode.OK, levels);
    }

    // PUT
    public HttpResponseMessage Put(PointsModel model)
    {
      /*if (!ModelState.IsValid)
      {
        return Request.CreateErrorResponse(
            HttpStatusCode.BadRequest, ModelState);
      }
      var level = _repositoryPoints.GetSingle(model.Id);
      if (level == null)
       {
         return Request.CreateResponse(HttpStatusCode.NotFound);
       }
      level.LevelId = model.LevelId;
      level.NextLevelId = model.NextLevelId;
      _repositoryPoints.Edit(level);
      _repositoryPoints.Save();*/
      return Request.CreateResponse(HttpStatusCode.OK);
    }
  }
}
