using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Sandbox.DAL;
using System.Data.Entity;

namespace Sandbox
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            using(var context = new BookContext())
            {
                Database.SetInitializer(new BookInitializer());
                context.Database.Initialize(true);
            }
        }
    }
}
