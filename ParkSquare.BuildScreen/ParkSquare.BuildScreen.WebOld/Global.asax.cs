using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;

namespace ParkSquare.BuildScreen.WebOld
{
    public class MvcApplication : HttpApplication
    {
        private static IWindsorContainer _windsorContainer;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Bootstrap();
        }

        public void Bootstrap()
        {
            _windsorContainer = Bootstrapper.Bootstrap();
        }


        protected void Application_End()
        {
            _windsorContainer.Dispose();
        }

    }
}