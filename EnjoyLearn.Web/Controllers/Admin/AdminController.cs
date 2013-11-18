using EnjoyLearn.Data.Core.Interfaces;
using EnjoyLearn.Models;

namespace EnjoyLearn.Web.Controllers.Admin
{
  
  using NLog.Mvc;
  using System;
  using System.Linq;
  using System.Web.Mvc;

  [Authorize(Roles = "Administrator")]
  public class AdminController : BaseController
  {
    //private readonly IEntityRepository<LogModel> _logEntryRepository;

    public AdminController(ILogger logger)//, IEntityRepository<LogModel> logEntryRepository)
      : base(logger)
    {

     // this._logEntryRepository = logEntryRepository;
    }

    public ActionResult Index()
    {
      try
      {
       // var model = _logEntryRepository.GetAll().OrderByDescending(e => e.TimeStamp).AsEnumerable();
        return View();
      }
      catch (Exception)
      {
        Logger.Information("Exelent!!!");
        return View("/");
      }

    }

  }
}
