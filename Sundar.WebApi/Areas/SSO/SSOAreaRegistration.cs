using System.Web.Mvc;

namespace Sundar.WebApi.Areas.SSO
{
    /// <summary>
    /// SSO区域注册
    /// </summary>
    public class SSOAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SSO";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SSO_default",
                "SSO/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}