using System.Web;
using System.Web.Optimization;

namespace EnjoyLearn.Web
{
  public class BundleConfig
  {
    // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
    public static void RegisterBundles(BundleCollection bundles)
    {
      // Script Bundles

      /*bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                 "~/Scripts/angular/angular.js",
                 "~/Scripts/angular/angular-resource.js"));
      */

      bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                   "~/Scripts/admin/hideshow.js",
                   "~/Scripts/admin/jquery.equalHeight.js",
                   "~/Scripts/admin/jquery.tablesorter.min.js",
                    "~/Scripts/admin/main.js"));

      bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                  "~/Scripts/angular/angular.js",
                  "~/Scripts/bootstrap/ui-bootstrap-tpls-{version}.js",
                  "~/Scripts/bootstrap/ui.validate.js",
                  "~/Scripts/angular/angular-strap.js",
                  "~/Scripts/angular/angular-bootstrap.js",
                  "~/Scripts/angular/angular-bootstrap-prettify.js",
                  //"~/Scripts/angular/angular-scenario.js",
                  "~/Scripts/angular/angular-resource.js"));

      bundles.Add(new ScriptBundle("~/bundles/localize").Include(
                 "~/Scripts/localize/localize.js"));

      bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                  "~/Scripts/jquery-{version}.js"));

      bundles.Add(new ScriptBundle("~/bundles/signalR").Include(
                 "~/Scripts/jquery/jquery.signalR-{version}.js"));

      bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                  "~/Scripts/jquery-ui-{version}.js"));

      bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                  "~/Scripts/jquery/jquery.unobtrusive*",
                  "~/Scripts/jquery/jquery.validate*"));

      bundles.Add(new ScriptBundle("~/bundles/underscore")
                .Include("~/Scripts/underscore/underscore.js"));

      // Use the development version of Modernizr to develop with and learn from. Then, when you're
      // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
      bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                  "~/Scripts/bootstrap/modernizr-*"));

      bundles.Add(new ScriptBundle("~/bundles/openid").Include(
                  "~/Scripts/openid/openid-jquery.js",
                  "~/Scripts/openid/openid-en.js"));

      // LESS CSS Bundles

      bundles.Add(new StyleBundle("~/Content/themes/theme1")
          .Include("~/Content/themes/theme1/less/style.less"));

      // CSS Bundles


    

        
      bundles.Add(new Bundle("~/Content/bootstrap")
        .Include("~/Content/bootstrap/bootstrap.css"));

      bundles.Add(new Bundle("~/Content/themes/base")
        .Include("~/Content/themes/base/jquery.ui.all.css"));

      bundles.Add(new Bundle("~/Content/themes/admin")
          .Include("~/Content/themes/Admin/css/layout.css",
          "~/Content/themes/Admin/css/ie.css"));

      bundles.Add(new Bundle("~/Content/css-two")
          .Include("~/Content/bootstrap/bootstrap-responsive.css")
          .Include("~/Content/font-awesome/font-awesome.css")
          .Include("~/Content/app/main.css"));

      bundles.Add(new StyleBundle("~/Content/openid").Include(
                   "~/Content/openid/css/openid-shadow.css",
                   "~/Content/openid/css/openid.css"));

   


    }
  }
}