using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using DotNetOpenAuth.AspNet.Clients;


namespace EnjoyLearn.Web.AuthOpenID.Clients
{
  public class MailRuClient : OAuth2Client
  {
    #region Constants and Fields

    /// <summary>
    /// The authorization endpoint.
    /// </summary>
    private const string AuthorizationEndpoint = "https://connect.mail.ru/oauth/authorize";

    /// <summary>
    /// The token endpoint.
    /// </summary>
    private const string TokenEndpoint = "https://connect.mail.ru/oauth/token";

    /// <summary>
    /// The UserInfo endpoint.
    /// </summary>
    private const string UserInfoEndpoint = "http://www.appsmail.ru/platform/api";

    /// <summary>
    /// The _app id.
    /// </summary>
    private readonly string _appId;

    /// <summary>
    /// The _app secret.
    /// </summary>
    private readonly string _appSecret;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FacebookClient"/> class.
    /// </summary>
    /// <param name="appId">
    /// The app id.
    /// </param>
    /// <param name="appSecret">
    /// The app secret.
    /// </param>
    public MailRuClient(string appId, string appSecret)
      : base("MailRu")
    {
      if (String.IsNullOrEmpty(appId)) throw new ArgumentNullException("appId");
      if (String.IsNullOrEmpty(appSecret)) throw new ArgumentNullException("appSecret");

      _appId = appId;
      _appSecret = appSecret;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The get service login url.
    /// </summary>
    /// <param name="returnUrl">
    /// The return url.
    /// </param>
    /// <returns>An absolute URI.</returns>
    protected override Uri GetServiceLoginUrl(Uri returnUrl)
    {
      // Note: Facebook doesn't like us to url-encode the redirect_uri value
      var builder = new UriBuilder(AuthorizationEndpoint);
      builder.AppendQueryArgs(
          new Dictionary<string, string> {
					{ "client_id", _appId },
					{ "redirect_uri", NormalizeHexEncoding(returnUrl.AbsoluteUri )},
          {"scope", "stream"},
          {"response_type", "code"}
				});
      return builder.Uri;
    }

    /// <summary>
    /// Returns MD5 Hash of input.
    /// </summary>
    /// <param name="input">The line.</param>
    public static string GetMd5Hash(string input)
    {
      var provider = new MD5CryptoServiceProvider();
      var bytes = Encoding.UTF8.GetBytes(input);
      bytes = provider.ComputeHash(bytes);
      return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
    }

    /// <summary>
    /// The get user data.
    /// </summary>
    /// <param name="accessToken">
    /// The access token.
    /// </param>
    /// <returns>A dictionary of profile data.</returns>
    protected override IDictionary<string, string> GetUserData(string accessToken)
    {
      if (accessToken == null) throw new ArgumentNullException("accessToken");
      var data = HttpContext.Current.Session["MailRuOAuthProvider.Token"] as MailRuTokenResponse;
      if (data == null || data.AccessToken != accessToken)
        return null;
      var res = new Dictionary<string, string>
                {
                    {"id", data.UserId.ToString(CultureInfo.InvariantCulture)}
                };
      var builder = new UriBuilder(UserInfoEndpoint);
      var signature = string.Format( "app_id={0}method=users.getInfosecure=1session_key={1}{2}",_appId,accessToken,_appSecret);
      signature = GetMd5Hash(signature);
      var args = new Dictionary<string, string>
                {
                    {"app_id",_appId},
                    {"method", "users.getInfo"},
                    {"secure", "1"},
                    {"session_key", accessToken},
                    {"sig", signature}
                };
      builder.AppendQueryArgs(args);
      using (var client = new WebClient())
      {
        using (var stream = client.OpenRead(builder.Uri))
        {
          var serializer = new DataContractJsonSerializer(typeof(MailRuDataResponse));
          if (stream != null)
          {
            var info = (MailRuDataResponse)serializer.ReadObject(stream);
            if (info.Items != null && info.Items.Any())
            {
              var item = info.Items[0];
              res.AddItemIfNotEmpty("username", item.Username);
              res.AddItemIfNotEmpty("name", (((item.FirstName ?? "") + " " + (item.LastName ?? "")).Trim()));
              res.AddItemIfNotEmpty("birthday", item.Birthdate);
              res.AddItemIfNotEmpty("gender", item.Sex);
              res.AddItemIfNotEmpty("photo", item.PhotoBig ?? item.Photo);
              res.AddItemIfNotEmpty("phone", item.PhoneMobile ?? item.PhoneHome ?? item.Phone);
            }
          }
        }
      }
      return res;
    }

    /// <summary>
    /// Obtains an access token given an authorization code and callback URL.
    /// </summary>
    /// <param name="returnUrl">
    /// The return url.
    /// </param>
    /// <param name="authorizationCode">
    /// The authorization code.
    /// </param>
    /// <returns>
    /// The access token.
    /// </returns>
    protected override string QueryAccessToken(Uri returnUrl, string authorizationCode)
    {
      var builder = new UriBuilder(TokenEndpoint);
      var args = new NameValueCollection
                {
                    {"code", authorizationCode},
                    {"redirect_uri", NormalizeHexEncoding(returnUrl.AbsoluteUri)},
                    {"grant_type", "authorization_code"},
                    {"client_id", _appId},
                    {"client_secret", _appSecret},
                };

      using (var client = new WebClient())
      {
        using (var stream = new MemoryStream(client.UploadValues(builder.Uri, "POST", args)))
        {
          var serializer = new DataContractJsonSerializer(typeof(MailRuTokenResponse));
          var data = (MailRuTokenResponse)serializer.ReadObject(stream);
          HttpContext.Current.Session["MailRuOAuthProvider.Token"] = data;
          return data.AccessToken;
        }
      }
    }

    /// <summary>
    /// Converts any % encoded values in the URL to uppercase.
    /// </summary>
    /// <param name="url">The URL string to normalize</param>
    /// <returns>The normalized url</returns>
    /// <example>NormalizeHexEncoding("Login.aspx?ReturnUrl=%2fAccount%2fManage.aspx") returns "Login.aspx?ReturnUrl=%2FAccount%2FManage.aspx"</example>
    /// <remarks>
    /// There is an issue in Facebook whereby it will rejects the redirect_uri value if
    /// the url contains lowercase % encoded values.
    /// </remarks>
    private static string NormalizeHexEncoding(string url)
    {
      var chars = url.ToCharArray();
      for (int i = 0; i < chars.Length - 2; i++)
      {
        if (chars[i] == '%')
        {
          chars[i + 1] = char.ToUpperInvariant(chars[i + 1]);
          chars[i + 2] = char.ToUpperInvariant(chars[i + 2]);
          i += 2;
        }
      }
      return new string(chars);
    }

    #endregion
  }

  [DataContract, EditorBrowsable(EditorBrowsableState.Never)]
  internal class MailRuTokenResponse
  {
    [DataMember(Name = "access_token")]
    public string AccessToken { get; set; }
    [DataMember(Name = "user_id")]
    public long UserId { get; set; }
    [DataMember(Name = "expires_in")]
    public long ExpiresIn { get; set; }
  }

  [DataContract, EditorBrowsable(EditorBrowsableState.Never)]
  internal class MailRuDataResponse
  {
    [DataMember(Name = "response")]
    public MailRuDataItem[] Items { get; set; }
  }

  [DataContract, EditorBrowsable(EditorBrowsableState.Never)]
  internal class MailRuDataItem
  {
    [DataMember(Name = "uid")]
    public long UserId { get; set; }
    [DataMember(Name = "sex")]
    public string Sex { get; set; }
    [DataMember(Name = "screen_name")]
    public string Username { get; set; }
    [DataMember(Name = "nickname")]
    public string Nickname { get; set; }
    [DataMember(Name = "first_name")]
    public string FirstName { get; set; }
    [DataMember(Name = "last_name")]
    public string LastName { get; set; }
    [DataMember(Name = "bdate")]
    public string Birthdate { get; set; }
    [DataMember(Name = "photo")]
    public string Photo { get; set; }
    [DataMember(Name = "photo_big")]
    public string PhotoBig { get; set; }
    [DataMember(Name = "phone")]
    public string Phone { get; set; }
    [DataMember(Name = "mobile_phone")]
    public string PhoneMobile { get; set; }
    [DataMember(Name = "home_phone")]
    public string PhoneHome { get; set; }
  }

}