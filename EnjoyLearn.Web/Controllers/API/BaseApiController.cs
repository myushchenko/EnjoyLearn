using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EnjoyLearn.Models.Tranings;
using EnjoyLearn.Web.Filters;
using NLog.Mvc;
using WebMatrix.WebData;

namespace EnjoyLearn.Web.Controllers.API
{
  [InitializeSimpleMembership]
  public abstract class BaseApiController : ApiController
  {
    public ILogger Logger = null;
    protected BaseApiController(ILogger logger)
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
