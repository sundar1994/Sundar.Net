using Sundar.BLL;
using Sundar.BLL.Response;
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

        /// <summary>
        /// datatable结构的模块列表
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public string GetModules(string pId)
        {
            string cascadeId = ".0.";
            if (!string.IsNullOrEmpty(pId))
            {
                //var obj = user.Modules.SingleOrDefault(u => u.Id == pId);
                //if (obj == null)
                //    throw new Exception("未能找到指定对象信息");
                //cascadeId = obj.CascadeId;
            }

            var query = ModuleBLL.LoadForUser("").Where(u => u.CascadeId.Contains(cascadeId));

            return JsonHelper.Instance.Serialize(new TableData
            {
                data = query.ToList(),
                count = query.Count(),
            });
        }

        /// <summary>
        /// 获取用户可访问的模块列表
        /// </summary>
        public string QueryModuleList()
        {
            var orgs = ModuleBLL.LoadForUser("").MapToList<ModuleView>();
            return JsonHelper.Instance.Serialize(orgs);
        }

        public ActionResult Test(int account)
        {
            int a = ModuleBLL.Get(account);
            return Content(a.ToString());
        }
    }
}