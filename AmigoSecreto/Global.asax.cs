using System.Web.Optimization;
using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using AmigoSecreto;
using AmigoSecretoSystem.Helpers;

namespace AmigoSecretoSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void Application_Error(object sender, EventArgs e)
        {
            var erro = Server.GetLastError();
            Auxiliares.GravaLogErro(erro);
        }
    }
}
