using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EnjoyLearn.Models.Authorize;
using EnjoyLearn.Web.Models;
using WebMatrix.WebData;

namespace EnjoyLearn.Web.Controllers.API
{
  public class SessionsController : ApiController
    {
        private readonly Func<string, string, bool, bool> signIn;
        private readonly Action signOut;

        public SessionsController() :
            this(WebSecurity.Login, WebSecurity.Logout)
        {
        }

        public SessionsController(
            Func<string, string, bool, bool> signIn,
            Action signOut)
        {
            this.signIn = signIn;
            this.signOut = signOut;
        }

        public HttpResponseMessage Post(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, ModelState);
            }

            var success = signIn(
                model.UserName.ToLowerInvariant(),
                model.Password,
                model.RememberMe);

            return Request.CreateResponse(success ?
                HttpStatusCode.NoContent :
                HttpStatusCode.BadRequest);
        }

        [Authorize]
        public HttpResponseMessage Delete()
        {
            signOut();
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}