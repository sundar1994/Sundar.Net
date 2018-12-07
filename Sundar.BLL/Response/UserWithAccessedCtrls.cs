using Sundar.Repository.Domain;
using System.Collections.Generic;

namespace Sundar.BLL.Response
{
    /// <summary>
    ///  视图模型
    /// <para>包括用户及用户可访问的机构/资源/模块</para>
    /// </summary>
    public class UserWithAccessedCtrls
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public Sys_User User { get; set; }

        /// <summary>
        /// 用户可以访问到的模块（包括所属角色与自己的所有模块）
        /// </summary>
        public List<ModuleView> Modules { get; set; }

        /// <summary>
        /// 用户可以访问的资源
        /// </summary>
        public List<Resource> Resources { get; set; }

        /// <summary>
        ///  用户所属机构
        /// </summary>
        public List<Sys_Org> Orgs { get; set; }

        /// <summary>
        /// 用户所属角色
        /// </summary>
        public List<Sys_Role> Roles { get; set; }
    }
}
