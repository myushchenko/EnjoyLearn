using EnjoyLearn.Web.App_Start;
using EnjoyLearn.Web.IoC.Autofac;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EnjoyLearn.Web
{
  public class MvcApplication : System.Web.HttpApplication
  {
    private static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

    public MvcApplication()
    {
      EndRequest += new EventHandler(OnEndRequest);
      BeginRequest += new EventHandler(OnBeginRequest);
    }

    protected void Application_Start()
    {
      // Web API Config
      HttpConfiguration httpConfiguration = GlobalConfiguration.Configuration;
      AutofacWebApi.Initialize(httpConfiguration);
      WebApiConfig.Register(GlobalConfiguration.Configuration);
      WebApiConfig.Configure(httpConfiguration);

      // SignalR Config
      AutofacSignalR.Initialize();
      RouteTable.Routes.MapHubs();

      // MVC Config
      AutofacMvc.Initialize();
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);

      // Bundle Config
      //// Force optimization to be on or off, regardless of web.config setting
      //BundleTable.EnableOptimizations = false;
      BundleConfig.RegisterBundles(BundleTable.Bundles);
      AuthConfig.RegisterAuth();
     
    }

    void OnBeginRequest(object sender, EventArgs e)
    {
      //log.Info("OnBeginRequest");
    }

    void OnEndRequest(object sender, EventArgs e)
    {
      //log.Info("OnEndRequest");
    }

    //public void Init()
    //{
    //  log.Info("Application Init");
    //}

    //public void Dispose()
    //{
    //  log.Info("Application Dispose");
    //}

    //protected void Application_Error()
    //{
    //  log.Info("Application Error");
    //}

    //protected void Application_End()
    //{
    //  log.Info("Application End");
    //}

    //protected void Session_Start()
    //{
    //  log.Info("Session Start");
    //}

    //protected void Session_End()
    //{
    //  log.Info("Session End");
    //}
  }
}