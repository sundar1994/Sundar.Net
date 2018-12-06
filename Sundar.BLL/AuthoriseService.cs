using Sundar.Repository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sundar.BLL
{
    /// <summary>
    /// 领域服务
    /// <para>用户授权服务</para>
    /// </summary>
    public class AuthoriseService : BaseApp<Sys_User>
    {

        protected Sys_User _user;

        private List<int> _userRoleIds;    //用户角色GUID

        public List<Sys_Module> Modules
        {
            get { return GetModulesQuery().ToList(); }
        }

        public List<Sys_Role> Roles
        {
            get { return GetRolesQuery().ToList(); }
        }

        public List<Sys_ModuleElement> ModuleElements
        {
            get { return GetModuleElementsQuery().ToList(); }
        }

        public List<Resource> Resources
        {
            get { return GetResourcesQuery().ToList(); }
        }

        public List<Sys_Org> Orgs
        {
            get { return GetOrgsQuery().ToList(); }
        }

        public Sys_User User
        {
            get { return _user; }
            set
            {
                _user = value;
                _userRoleIds = UnitWork.Find<Relevance>(u => u.FirstId == _user.Id && u.Key == Define.USERROLE).Select(u => u.SecondId).ToList();
            }
        }

        public void Check(string userName, string password)
        {
            var _user = Repository.FindSingle(u => u.Account == userName);
            if (_user == null)
            {
                throw new Exception("用户帐号不存在");
            }
            _user.CheckPassword(password);
        }

        /// <summary>
        /// 用户可访问的机构
        /// </summary>
        /// <returns>IQueryable&lt;Org&gt;.</returns>
        public virtual IQueryable<Sys_Org> GetOrgsQuery()
        {
            var orgids = UnitWork.Find<Relevance>(
                u =>
                    (u.FirstId == _user.Id && u.Key == Define.USERORG) ||
                    (u.Key == Define.ROLEORG && _userRoleIds.Contains(u.FirstId))).Select(u => u.SecondId);
            return UnitWork.Find<Sys_Org>(u => orgids.Contains(u.Id));
        }

        /// <summary>
        /// 获取用户可访问的资源
        /// </summary>
        /// <returns>IQueryable&lt;Resource&gt;.</returns>
        public virtual IQueryable<Resource> GetResourcesQuery()
        {
            var resourceIds = UnitWork.Find<Relevance>(
                u =>
                    (u.FirstId == _user.Id && u.Key == Define.USERRESOURCE) ||
                    (u.Key == Define.ROLERESOURCE && _userRoleIds.Contains(u.FirstId))).Select(u => u.SecondId);
            return UnitWork.Find<Resource>(u => resourceIds.Contains(u.Id));
        }

        /// <summary>
        /// 模块菜单权限
        /// </summary>
        public virtual IQueryable<Sys_ModuleElement> GetModuleElementsQuery()
        {
            var elementIds = UnitWork.Find<Relevance>(
                u =>
                    (u.FirstId == _user.Id && u.Key == Define.USERELEMENT) ||
                    (u.Key == Define.ROLEELEMENT && _userRoleIds.Contains(u.FirstId))).Select(u => u.SecondId);
            return UnitWork.Find<Sys_ModuleElement>(u => elementIds.Contains(u.Id));
        }

        /// <summary>
        /// 得出最终用户拥有的模块
        /// </summary>
        public virtual IQueryable<Sys_Module> GetModulesQuery()
        {
            var moduleIds = UnitWork.Find<Relevance>(
                u =>
                    (u.FirstId == _user.Id && u.Key == Define.USERMODULE) ||
                    (u.Key == Define.ROLEMODULE && _userRoleIds.Contains(u.FirstId))).Select(u => u.SecondId);
            return UnitWork.Find<Sys_Module>(u => moduleIds.Contains(u.Id)).OrderBy(u => u.SortNo);
        }

        //用户角色
        public virtual IQueryable<Sys_Role> GetRolesQuery()
        {
            return UnitWork.Find<Sys_Role>(u => _userRoleIds.Contains(u.Id));
        }
    }
}
