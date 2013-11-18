
namespace EnjoyLearn.Web.Controllers
{
  using EnjoyLearn.Web.Filters;
  using NLog.Mvc;
  using System.Web.Mvc;
  using WebMatrix.WebData;

  [InitializeSimpleMembership]
  public abstract class BaseController : Controller
  {
    public ILogger Logger = null;
    protected BaseController(ILogger logger)
    {
      this.Logger = logger;
      if (!WebSecurity.Initialized)
      {
        WebSecurity.InitializeDatabaseConnection(
             "EnjoyLearnDB",
             "Users",
             "UserId",
             "UserName",
             autoCreateTables: true);
      }
    }
  }
}
