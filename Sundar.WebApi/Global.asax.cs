using Sundar.BLL;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sundar.WebApi
{
    /// <summary>
    /// APP启动
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// API启动
        /// </summary>
        protected void Application_Start()
        {
            AutofacExt.InitAutofac();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
