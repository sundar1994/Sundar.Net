using System;
using System.Linq;
using Sundar.Repository.Domain;

namespace Sundar.BLL
{
    /// <summary>
    /// 领域服务
    /// <para>超级管理员权限</para>
    /// </summary>
    public class SystemAuthService : AuthoriseService
    {
        public SystemAuthService()
        {
            _user = new Sys_User
            {
                Account = "System",
                Name = "超级管理员",
                Id = 0
            };
        }



        public override IQueryable<Sys_Org> GetOrgsQuery()
        {
            return UnitWork.Find<Sys_Org>(null);
        }

        public override IQueryable<Resource> GetResourcesQuery()
        {
            return UnitWork.Find<Resource>(null);
        }

        public override IQueryable<Sys_ModuleElement> GetModuleElementsQuery()
        {
            return UnitWork.Find<Sys_ModuleElement>(null);
        }

        public override IQueryable<Sys_Module> GetModulesQuery()
        {
            return UnitWork.Find<Sys_Module>(null);
        }

        public override IQueryable<Sys_Role> GetRolesQuery()
        {
            //用户角色
            return UnitWork.Find<Sys_Role>(null);
        }
    }
}
