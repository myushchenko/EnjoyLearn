using System;
using System.Collections.Generic;
using EnjoyLearn.Web.AuthOpenID.Clients;
using Microsoft.Web.WebPages.OAuth;

namespace EnjoyLearn.Web.AuthOpenID
{
    public static class OAuthWebSecurityExt
    {

        public static void RegisterVkClient(string appId, string appSecret, Dictionary<string, object> extraData)
        {
            if (!String.IsNullOrEmpty(appId) && !String.IsNullOrEmpty(appSecret))
                OAuthWebSecurity.RegisterClient(
                    new VkClient(appId, appSecret),
                    "Vkontakte",
                    extraData);
        }

        public static void RegisterOdnoklassnikiClient(string appId, string appSecret, string appPublic, Dictionary<string, object> extraData)
        {
            if (!String.IsNullOrEmpty(appId) && !String.IsNullOrEmpty(appSecret) && !String.IsNullOrEmpty(appPublic))
                OAuthWebSecurity.RegisterClient(
                    new OdnoklassnikiClient(appId, appSecret, appPublic),
                    "Odnoklassniki",
                    extraData);
        }

        public static void RegisterMailRuClientClient(string appId, string appSecret, Dictionary<string, object> extraData)
        {
            if (!String.IsNullOrEmpty(appId) && !String.IsNullOrEmpty(appSecret))
                OAuthWebSecurity.RegisterClient(
                    new MailRuClient(appId, appSecret),
                    "MailRu",
                   extraData);
        }

    }
}