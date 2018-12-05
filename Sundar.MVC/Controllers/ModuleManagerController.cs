using Sundar.BLL;
using Sundar.BLL.Response;
using Sundar.BLL.SSO;
using Sundar.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sundar.MVC.Controllers
{
    /// <summary>
    /// 模块管理
    /// </summary>
    public class ModuleManagerController : BaseController
    {
        public ModuleManagerBLL ModuleBLL { get; set; }

        /// <summary>
        /// 模块管理主页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 根据某用户ID获取可访问某模块的菜单项
        /// </summary>
        /// <returns></returns>
        public string LoadMenusForUser(int moduleId, string firstId)
        {
            var menus = ModuleBLL.LoadMenusForUser(moduleId, firstId);
            return JsonHelper.Instance.Serialize(menus);
        }

        /// <summary>
        /// 获取发起页面的菜单权限
        /// </summary>
        /// <returns>System.String.</returns>
        public string LoadAuthorizedMenus(string modulecode)
        {
            var user = new AuthUtil().GetCurrentUser();
            var module = user.Modules.First(u => u.Code == modulecode);
            if (module != null)
            {
                return JsonHelper.Instance.Serialize(module.Elements);
            }
            return "";
        }

        /// <summary>
        /// 加载当前用户可访问模块的菜单
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <returns>System.String.</returns>
        public string LoadMenus(int moduleId)
        {
            var user =new AuthUtil().GetCurrentUser();

            var module = user.Modules.Single(u => u.Id == moduleId);

            var data = new TableData
            {
                data = module.Elements,
                count = module.Elements.Count(),
            };
            return JsonHelper.Instance.Serialize(data);
        }
    }
}