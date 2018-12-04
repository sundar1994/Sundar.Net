using Sundar.Repository.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Sundar.BLL
{
    public class OrgManagerBLL : BaseApp<Sys_Org>
    {
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="org">The org.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="System.Exception">未能找到该组织的父节点信息</exception>
        public int Add(Sys_Org org)
        {
            ChangeModuleCascade(org);

            Repository.Add(org);

            return org.Id;
        }
    }
}
