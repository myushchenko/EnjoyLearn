using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using System.Web.Security;
using ActionMailer.Net.Mvc;

namespace EnjoyLearn.Web.Controllers.MVC
{
    public class MailController : MailerBase
    {
        //
        // GET: /Mail/

      public EmailResult VerificationEmail(MembershipUser model)
      {
        To.Add("pentium4.misha@gmail.com"); //(model.EmailAddress);
        From = "yushchenko.michael@gmail.com";
        Subject = "Welcome to My Cool Site!";
        return Email("VerificationEmail", model);
      }

    }
}
