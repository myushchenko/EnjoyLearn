using System.Web.Mvc;
using NLog.Mvc;

namespace EnjoyLearn.Web.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());

            var logger = DependencyResolver.Current.GetService<ILogger>();
            filters.Add(new NLogMvcHandleErrorAttribute(logger));
        }
    }
}