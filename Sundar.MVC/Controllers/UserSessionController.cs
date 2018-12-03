using Sundar.BLL;
using Sundar.Common;
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
            var moduleTree = ModuleBLL.LoadForUser("").GenerateTree(u => u.Id, u => u.ParentId);
            
            return JsonHelper.Instance.Serialize(moduleTree);
        }

        public ActionResult Get(int account)
        {
            int a = ModuleBLL.Get(account);
            return Content(a.ToString());
        }
    }
}