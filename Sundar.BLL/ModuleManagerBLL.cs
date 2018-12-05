using Sundar.Repository.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Sundar.BLL
{
    public class ModuleManagerBLL : BaseApp<Sys_Module>
    {
        /// <summary>
        /// 新增模块
        /// </summary>
        /// <param name="model"></param>

        public void Add(Sys_Module model)
        {
            ChangeModuleCascade(model);
            Repository.Add(model);
        }

        /// <summary>
        /// 更新模块
        /// </summary>
        /// <param name="model"></param>
        public void Update(Sys_Module model)
        {
            ChangeModuleCascade(model);
            Repository.Update(u => u.Id, model);
        }

        #region 用户/角色分配模块

        /// <summary>
        /// 加载特定用户的模块
        /// TODO:这里会加载用户及用户角色的所有模块，“为用户分配模块”功能会给人一种混乱的感觉，但可以接受
        /// </summary>
        /// <param name="userId">The user unique identifier.</param>
        public IEnumerable<Sys_Module> LoadForUser(string userId)
        {
            // var roleIds = RevelanceManagerApp.Get(Define.USERROLE, true, userId);
            var moduleIds = UnitWork.Find<Sys_Module>(u => u.Id > 0).ToList();
            return moduleIds;
        }

        /// <summary>
        /// 根据某用户ID获取可访问某模块的菜单项
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<Sys_ModuleElement> LoadMenusForUser(int moduleId, string userId)
        {
            // var elementIds = RevelanceManagerApp.Get(Define.USERELEMENT, true, userId);
            return UnitWork.Find<Sys_ModuleElement>(u => u.Id > 0 && u.ModuleId == moduleId);
        }


        #endregion

        #region 菜单操作
        /// <summary>
        /// 删除指定的菜单
        /// </summary>
        /// <param name="ids"></param>
        public void DelMenu(int[] ids)
        {
            UnitWork.Delete<Sys_ModuleElement>(u => ids.Contains(u.Id));
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="model"></param>
        public void AddMenu(Sys_ModuleElement model)
        {
            UnitWork.Add(model);
            UnitWork.Save();
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="model"></param>
        public void UpdateMenu(Sys_ModuleElement model)
        {
            UnitWork.Update<Sys_ModuleElement>(u => u.Id, model);
            UnitWork.Save();
        }
        #endregion
    }
}
