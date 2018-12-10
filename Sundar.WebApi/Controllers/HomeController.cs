using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sundar.WebApi.Controllers
{
    /// <summary>
    /// 主控制器
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 在线文档跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return Redirect("/Swagger/ui/index");
        }
    }
}