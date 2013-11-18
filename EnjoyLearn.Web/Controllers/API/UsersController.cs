using EnjoyLearn.Data.Core.Interfaces;
using EnjoyLearn.Models.Authorize;

using EnjoyLearn.Web.Infrastructure;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using EnjoyLearn.Web.Models;
using WebMatrix.WebData;

namespace EnjoyLearn.Web.Controllers.API
{
  public class UsersController : ApiController
  {
    private readonly Func<string, string, bool, string> signup;
    private readonly IMailer mailer;

    private bool? debuggingEnabled;

    private readonly IEntityRepository<UserProfileModel> _userProfileRepository;

    public UsersController(IEntityRepository<UserProfileModel> userProfileRepository)
      : this(
          (userName, password, requireConfirmation) =>
              WebSecurity.CreateUserAndAccount(
              userName,
              password,
              requireConfirmationToken: requireConfirmation),
          new Mailer())
    {
      this._userProfileRepository = userProfileRepository;
    }

    public UsersController(
        Func<string, string, bool, string> signup,
        IMailer mailer)
    {
      if (!WebSecurity.Initialized)
      {
        WebSecurity.InitializeDatabaseConnection(
             "EnjoyLearnDB",
             "Users",
             "UserId",
             "UserName",
             autoCreateTables: true);
      }

      this.signup = signup;
      this.mailer = mailer;
    }

    public bool IsDebuggingEnabled
    {
      get
      {
        if (debuggingEnabled == null)
        {
          object context;

          if (Request.Properties.TryGetValue("MS_HttpContext", out context))
          {
            var httpContext = context as HttpContextBase;

            debuggingEnabled = (httpContext != null) && httpContext.IsDebuggingEnabled;
          }
          else
          {
            debuggingEnabled = false;
          }
        }

        return debuggingEnabled.GetValueOrDefault();
      }

      set
      {
        debuggingEnabled = value;
      }
    }

    //POST
    public async Task<HttpResponseMessage> Post(RegisterModel model)
    {
      if (!ModelState.IsValid)
      {
        return Request.CreateErrorResponse(
            HttpStatusCode.BadRequest, ModelState);
      }

      var statusCode = MembershipCreateStatus.Success;
      var userName = model.UserName.ToLowerInvariant();
      var token = string.Empty;

      var requireConfirmation = !IsDebuggingEnabled;

      try
      {
        token = signup(userName, model.Password, requireConfirmation);
      }
      catch (MembershipCreateUserException e)
      {
        statusCode = e.StatusCode;
      }

      if (statusCode == MembershipCreateStatus.Success)
      {
        var newUserProfile = new UserProfileModel
          {
            Id = Guid.NewGuid(),
            FirstName = "First Name",
            LastName = "Last Name",
            UserId = WebSecurity.GetUserId(model.UserName)
          };

        _userProfileRepository.Add(newUserProfile);
        _userProfileRepository.Save();
        if (requireConfirmation)
        {
          await mailer.UserConfirmationAsync(userName, token);
        }

        return Request.CreateResponse(HttpStatusCode.NoContent);
      }

      switch (statusCode)
      {
        case MembershipCreateStatus.DuplicateUserName:
        case MembershipCreateStatus.DuplicateEmail:
        case MembershipCreateStatus.DuplicateProviderUserKey:
          ModelState.AddModelError(
              "email",
              "User with same name already exits.");
          break;
        case MembershipCreateStatus.InvalidUserName:
        case MembershipCreateStatus.InvalidEmail:
          ModelState.AddModelError(
              "email",
              "Invalid name address.");
          break;
        case MembershipCreateStatus.InvalidPassword:
          ModelState.AddModelError("password", "Invalid password.");
          break;
        default:
          ModelState.AddModelError(
              string.Empty,
              "Unexpected error.");
          break;
      }

      return Request.CreateErrorResponse(
          HttpStatusCode.BadRequest, ModelState);
    }
  }
}