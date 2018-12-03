using Sundar.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sundar.MVC.Controllers
{
    public class UserSessionController : Controller
    {
        public ModuleManagerBLL ModuleBLL { get; set; }

        /// <summary>
        /// 获取登录用户可访问的所有模块，及模块的操作菜单
        /// </summary>
        public string GetModulesTree()
        {
            return string.Empty;
        }

        public ActionResult Get(string account)
        {
            return Content(ModuleBLL.Get(account));
        }
    }
}