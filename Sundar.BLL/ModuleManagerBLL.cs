using Sundar.Repository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sundar.BLL
{
    public class ModuleManagerBLL : BaseApp<Sys_Module>
    {
        public int Get(int account)
        {
            return Repository.FindSingle(u => u.Id == account).Id;
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
        #endregion
    }
}
