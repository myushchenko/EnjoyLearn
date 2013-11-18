﻿namespace EnjoyLearn.Web.Helpers
{
  using System;
  using System.Collections.Concurrent;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  using System.Web.Hosting;
  using System.Web.Mvc;
  using System.Web.Mvc.Html;

  public static class ClientViewExtensions
  {
    private const string Prefix = "_";
    private const string ViewFolder = "ClientViews";
    private static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

    private static readonly
        ConcurrentDictionary<string, IEnumerable<string>> ClientViews =
            new ConcurrentDictionary<string, IEnumerable<string>>();

    public static void IncludeClientViews(
        this HtmlHelper instance,
        string folder = ViewFolder)
    {
      try
      {
        foreach (var template in GetClientViews(folder))
        {
          instance.RenderPartial(template);
        }
      }
      catch (Exception ex)
      {
        log.Error("ClientViewExtensions: " + ex);

      }
      
    }

    public static IHtmlString ClientView(
        this HtmlHelper instance,
        string viewName)
    {
      return ClientView(instance, ViewFolder, viewName);
    }

    public static IHtmlString ClientView(
        this HtmlHelper instance,
        string folder,
        string viewName)
    {
      return instance.Partial("../" + folder + "/" + Prefix + viewName);
    }

    private static IEnumerable<string> GetClientViews(string folder)
    {
      return ClientViews.GetOrAdd(folder, LoadClientViews);
    }

    private static IEnumerable<string> LoadClientViews(string folder)
    {
      var viewTemplateFolder = "~/Views/" + folder;

      return HostingEnvironment.VirtualPathProvider
                               .GetDirectory(viewTemplateFolder)
                               .Files
                               .Cast<VirtualFileBase>()
                               .Where(vfb =>
                                      (vfb.Name != null) &&
                                      !vfb.Name.StartsWith(Prefix, StringComparison.Ordinal))
                               .Select(vfd => vfd.VirtualPath)
                               .ToList();
    }
  }
}