using Sundar.BLL.Request;
using Sundar.BLL.Response;
using Sundar.BLL.SSO;
using Sundar.Repository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sundar.BLL
{
    public class RoleManagerBLL : BaseApp<Sys_Role>
    {
        public RevelanceManagerBLL ReleManagerApp { get; set; }

        /// <summary>
        /// 加载当前登录用户可访问的一个部门及子部门全部角色
        /// </summary>
        public TableData Load(QueryRoleListReq request)
        {
            var loginUser = AuthUtil.GetCurrentUser();

            string cascadeId = ".0.";
            if (request.orgId > 0)
            {
                var org = loginUser.Orgs.SingleOrDefault(u => u.Id == request.orgId);
                cascadeId = org.CascadeId;
            }

            var ids = loginUser.Orgs.Where(u => u.CascadeId.Contains(cascadeId)).Select(u => u.Id).ToArray();
            var roleIds = ReleManagerApp.Get(Define.ROLEORG, false, ids);

            var roles = UnitWork.Find<Sys_Role>(u => roleIds.Contains(u.Id))
                   .OrderBy(u => u.Name)
                   .Skip((request.page - 1) * request.limit)
                   .Take(request.limit);

            var records = Repository.GetCount(u => roleIds.Contains(u.Id));


            var roleViews = new List<RoleView>();
            foreach (var role in roles.ToList())
            {
                RoleView uv = role;
                var orgs = LoadByRole(role.Id);
                uv.Organizations = string.Join(",", orgs.Select(u => u.Name).ToList());
                uv.OrganizationIds = string.Join(",", orgs.Select(u => u.Id).ToList());
                roleViews.Add(uv);
            }

            return new TableData
            {
                count = records,
                data = roleViews,
            };
        }

        /// <summary>
        /// 加载角色的所有机构
        /// </summary>
        public IEnumerable<Sys_Org> LoadByRole(int roleId)
        {
            var result = from userorg in UnitWork.Find<Relevance>(null)
                         join org in UnitWork.Find<Sys_Org>(null) on userorg.SecondId equals org.Id
                         where userorg.FirstId == roleId && userorg.Key == Define.ROLEORG
                         select org;
            return result;
        }


        public void Add(RoleView obj)
        {
            if (obj.OrganizationIds == null)
                throw new Exception("请为角色分配机构");
            Sys_Role role = obj;
            role.CreateTime = DateTime.Now;
            Repository.Add(role);
            obj.Id = role.Id;   //要把保存后的ID存入view

            UpdateRele(obj);
        }

        public void Update(RoleView obj)
        {
            if (obj.OrganizationIds == null)
                throw new Exception("请为角色分配机构");
            Sys_Role role = obj;

            UnitWork.Update<Sys_Role>(u => u.Id == obj.Id, u => new Sys_Role
            {
                Name = role.Name,
                Status = role.Status
            });

            UpdateRele(obj);
        }

        /// <summary>
        /// 更新相应的多对多关系
        /// </summary>
        /// <param name="view"></param>
        private void UpdateRele(RoleView view)
        {
            int[] orgIds = Array.ConvertAll(view.OrganizationIds.Split(',').ToArray(), int.Parse);
            ReleManagerApp.DeleteBy(Define.ROLEORG, view.Id);
            ReleManagerApp.AddRelevance(Define.ROLEORG, orgIds.ToLookup(u => view.Id));
        }
    }
}
