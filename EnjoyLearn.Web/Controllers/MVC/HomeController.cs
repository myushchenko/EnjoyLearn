using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Transactions;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using EnjoyLearn.Data;
using EnjoyLearn.Models.Authorize;
using EnjoyLearn.Web.Models;
using Microsoft.Web.WebPages.OAuth;
using NLog.Mvc;
using WebMatrix.WebData;

namespace EnjoyLearn.Web.Controllers.MVC
{
  [AllowAnonymous]
  public class HomeController : BaseController
  {


    public HomeController(ILogger logger):base(logger)
    {
    }

    public ActionResult Index()
    {


    //  //try {
    //  //          // Initialize WebMail helper
    //  //          WebMail.SmtpServer = "smtp.gmail.com";
    //  //          WebMail.SmtpPort = 25;
    //  //          WebMail.EnableSsl = true;
    //  //          WebMail.UserName = "yushchenko.michael@gmail.com";
    //  //          WebMail.From = "yushchenko.michael@gmail.com";
    //  //          WebMail.Password = "19880694";
 
    //  //          // Send email
    //  //          WebMail.Send(to: "pentium4.misha@gmail.com",
    //  //          subject: "Test Subject",
    //  //          body: "Test Body"
    //  //          );
    //  //      }
    //  //      catch (Exception ex ) {
            
    //  //      }

    // /* if (User.Identity.IsAuthenticated)
    //  {
    //    //var user = Membership.GetUser(User.Identity.Name);

       // new MailController().VerificationEmail(null).Deliver();
    //  }*/

    // /* using (SmtpClient client = new SmtpClient())
    //  {
    //    MailMessage message = new MailMessage();
    //    message.From = new MailAddress("yushchenko.michael@gmail.com");
    //    message.To.Add(new MailAddress("pentium4.misha@gmail.com"));
    //    message.Subject = "theme";
    //    message.Body = "bodytext";
    //    client.Send(message);
    //  }*/

     return View();
    }

    //
    // POST: /Account/Disassociate

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Disassociate(string provider, string providerUserId)
    {
      string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
      AccountController.ManageMessageId? message = null;

      // Only disassociate the account if the currently logged in user is the owner
      if (ownerAccount == User.Identity.Name)
      {
        // Use a transaction to prevent the user from deleting their last login credential
        using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
        {
          bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
          if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
          {
            OAuthWebSecurity.DeleteAccount(provider, providerUserId);
            scope.Complete();
            message = AccountController.ManageMessageId.RemoveLoginSuccess;
          }
        }
      }

      return RedirectToAction("Manage", new { Message = message });
    }

    //
    // GET: /Account/Manage

    public ActionResult Manage(ManageMessageId? message)
    {
      ViewBag.StatusMessage =
          message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
          : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
          : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
          : "";
      ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
      ViewBag.ReturnUrl = Url.Action("Manage");
      return View();
    }

    //
    // POST: /Account/Manage

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Manage(LocalPasswordModel model)
    {
      bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
      ViewBag.HasLocalPassword = hasLocalAccount;
      ViewBag.ReturnUrl = Url.Action("Manage");
      if (hasLocalAccount)
      {
        if (ModelState.IsValid)
        {
          // ChangePassword will throw an exception rather than return false in certain failure scenarios.
          bool changePasswordSucceeded;
          try
          {
            changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
          }
          catch (Exception)
          {
            changePasswordSucceeded = false;
          }

          if (changePasswordSucceeded)
          {
            return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
          }
          else
          {
            ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
          }
        }
      }
      else
      {
        // User does not have a local password so remove any validation errors caused by a missing
        // OldPassword field
        ModelState state = ModelState["OldPassword"];
        if (state != null)
        {
          state.Errors.Clear();
        }

        if (ModelState.IsValid)
        {
          try
          {
            WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
            return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
          }
          catch (Exception e)
          {
            ModelState.AddModelError("", e);
          }
        }
      }

      // If we got this far, something failed, redisplay form
      return View(model);
    }

    //
    // POST: /Home/ExternalLogin?provider=twitter

    [HttpPost]
    [AllowAnonymous]
    //[ValidateAntiForgeryToken]
    public ActionResult ExternalLogin(string provider, string returnUrl)
    {
      String returnURI = Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl });

      return new ExternalLoginResult(provider, returnURI);
    }

    //
    // GET: /Account/ExternalLoginCallback

    [AllowAnonymous]
    public ActionResult ExternalLoginCallback(string returnUrl)
    {
      AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
      if (!result.IsSuccessful)
      {
        return RedirectToAction("ExternalLoginFailure");
      }

      if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
      {
        return RedirectToLocal(returnUrl);
      }

      if (User.Identity.IsAuthenticated)
      {
        // If the current user is logged in add the new account
        OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
        return RedirectToLocal(returnUrl);
      }
      else
      {
        // User is new, ask for their desired membership name
        string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
        ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
        ViewBag.ReturnUrl = returnUrl;
        return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = loginData });
      }
    }

    //
    // POST: /Account/ExternalLoginConfirmation

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
    {
      string provider = null;
      string providerUserId = null;

      if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
      {
        //return RedirectToAction("Manage");
      }

      if (ModelState.IsValid)
      {
        // Insert a new user into the database
        using (var db = new EnjoyLearnContext())
        {
          UserModel user = db.Users.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
          // Check if user already exists
          if (user == null)
          {
            // Insert name into the profile table
            db.Users.Add(new UserModel { UserName = model.UserName });
            db.SaveChanges();

            OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
            OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

            return RedirectToLocal(returnUrl);
          }
          else
          {
            ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name.");
          }
        }
      }

      ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
      ViewBag.ReturnUrl = returnUrl;
      return View(model);
    }

    //
    // GET: /Account/ExternalLoginFailure

    [AllowAnonymous]
    public ActionResult ExternalLoginFailure()
    {
      return View();
    }

    

    #region Helpers
    private ActionResult RedirectToLocal(string returnUrl)
    {
      if (Url.IsLocalUrl(returnUrl))
      {
        return Redirect(returnUrl);
      }
      else
      {
        return RedirectToAction("Index", "Home");
      }
    }

    public enum ManageMessageId
    {
      ChangePasswordSuccess,
      SetPasswordSuccess,
      RemoveLoginSuccess,
    }

    internal class ExternalLoginResult : ActionResult
    {
      public ExternalLoginResult(string provider, string returnUrl)
      {
        Provider = provider;
        ReturnUrl = returnUrl;
      }

      public string Provider { get; private set; }
      public string ReturnUrl { get; private set; }

      public override void ExecuteResult(ControllerContext context)
      {
        OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
        /*var url = System.Web.HttpContext.Current.Response.RedirectLocation;
        System.Web.HttpContext.Current.Response.Flush();
        System.Web.HttpContext.Current.Response.Redirect(url, false);*/
      }
    }

    private static string ErrorCodeToString(MembershipCreateStatus createStatus)
    {
      // See http://go.microsoft.com/fwlink/?LinkID=177550 for
      // a full list of status codes.
      switch (createStatus)
      {
        case MembershipCreateStatus.DuplicateUserName:
          return "User name already exists. Please enter a different user name.";

        case MembershipCreateStatus.DuplicateEmail:
          return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

        case MembershipCreateStatus.InvalidPassword:
          return "The password provided is invalid. Please enter a valid password value.";

        case MembershipCreateStatus.InvalidEmail:
          return "The e-mail address provided is invalid. Please check the value and try again.";

        case MembershipCreateStatus.InvalidAnswer:
          return "The password retrieval answer provided is invalid. Please check the value and try again.";

        case MembershipCreateStatus.InvalidQuestion:
          return "The password retrieval question provided is invalid. Please check the value and try again.";

        case MembershipCreateStatus.InvalidUserName:
          return "The user name provided is invalid. Please check the value and try again.";

        case MembershipCreateStatus.ProviderError:
          return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

        case MembershipCreateStatus.UserRejected:
          return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

        default:
          return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
      }
    }
    #endregion
  }

   
}
