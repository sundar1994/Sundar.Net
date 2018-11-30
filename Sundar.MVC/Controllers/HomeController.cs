using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sundar.MVC.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 后台主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 主页子页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Main()
        {
            return View();
        }
    }
}