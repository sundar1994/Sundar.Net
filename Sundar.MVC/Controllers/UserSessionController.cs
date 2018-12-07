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
    public class UserSessionController : Controller
    {
        UserWithAccessedCtrls user = AuthUtil.GetCurrentUser();
        // public ModuleManagerBLL ModuleBLL { get; set; }

        /// <summary>
        /// 获取登录用户可访问的所有模块，及模块的操作菜单
        /// </summary>
        public string GetModulesTree()
        {
            var moduleTree = user.Modules.GenerateTree(u => u.Id, u => u.ParentId);

            return JsonHelper.Instance.Serialize(moduleTree);
        }

        /// <summary>
        /// datatable结构的模块列表
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public string GetModules(int pId)
        {
            string cascadeId = ".0.";
            if (pId > 0)
            {
                var obj = user.Modules.SingleOrDefault(u => u.Id == pId);
                if (obj == null)
                    throw new Exception("未能找到指定对象信息");
                cascadeId = obj.CascadeId;
            }

            var query = user.Modules.Where(u => u.CascadeId.Contains(cascadeId));

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
            var orgs = user.Modules.MapToList<ModuleView>();
            return JsonHelper.Instance.Serialize(orgs);
        }

        /// <summary>
        /// 获取登录用户可访问的所有部门
        /// <para>用于树状结构</para>
        /// </summary>
        public string GetOrgs()
        {
            return JsonHelper.Instance.Serialize(user.Orgs);
        }
    }
}