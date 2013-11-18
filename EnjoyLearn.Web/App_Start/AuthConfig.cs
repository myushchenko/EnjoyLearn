namespace EnjoyLearn.Web.App_Start
{
  using AuthOpenID;
  using Microsoft.Web.WebPages.OAuth;
  using System.Collections.Generic;

  public static class AuthConfig
  {
    public static void RegisterAuth()
    {

      // Google
      var googleExtraData = new Dictionary<string, object> { { "social", "google" } };
      OAuthWebSecurity.RegisterGoogleClient("Google", googleExtraData);

      //Vkontakte
      var vkExtraData = new Dictionary<string, object> { { "social", "vkontakte" } };
      OAuthWebSecurityExt.RegisterVkClient(
         appId: "3679973",
         appSecret: "E1aiRY5qfa41JuzL2NCh",
        extraData: vkExtraData);

      //Odnoklassniki
      var odnoklassnikiExtraData = new Dictionary<string, object> { { "social", "odnoklassniki" } };
      OAuthWebSecurityExt.RegisterOdnoklassnikiClient(
             appId: "xxxxxxxxxxxxxxx",
             appSecret: "xxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
             appPublic: "",
        extraData: odnoklassnikiExtraData);

      //MailRu
      var mailRuExtraData = new Dictionary<string, object> { { "social", "mailru" } };
      OAuthWebSecurityExt.RegisterMailRuClientClient(
            appId: "706680",
            appSecret: "f7494b7a038b286154a3037c1be88e05",
            extraData: mailRuExtraData);

      /*
         appId: "706677",
            appSecret: "c307136260ea29ceac2cf42d8b114fc2",
       */

      // Facebook 
      var facebookExtraData = new Dictionary<string, object> { { "social", "facebook" } };
      OAuthWebSecurity.RegisterFacebookClient(
          appId: "170908326260300",
          appSecret: "baab76011f172503d528f2a30365cae2",
          displayName: "Facebook",
          extraData: facebookExtraData);

      // Twitter
      var twitterExtraData = new Dictionary<string, object> { { "social", "twitter" } };
      OAuthWebSecurity.RegisterTwitterClient(
        consumerKey: "ddNo8sCmUbpe2UaarIZkw",
        consumerSecret: "Lc9PKEpkrOIhfhsp5oTFpwbScmi7JTyDXcO5rZgSU",
        displayName: "Twitter",
        extraData: twitterExtraData);

      // LinkedIn
      var linkedInExtraData = new Dictionary<string, object> { { "social", "linkedin" } };
      OAuthWebSecurity.RegisterLinkedInClient(
              consumerKey: "aryibatk10ui",
              consumerSecret: "fcTTxms2jSTaFlV1",
              displayName: "LinkedIn",
              extraData: linkedInExtraData);

      // Yahoo
      var yahooExtraData = new Dictionary<string, object> { { "social", "yahoo" } };
      OAuthWebSecurity.RegisterYahooClient(
        displayName: "Yahoo",
        extraData: yahooExtraData);

    }
  }
}
