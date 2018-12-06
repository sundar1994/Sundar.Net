using Sundar.BLL.Request;
using Sundar.BLL.Response;
using Sundar.BLL.SSO;
using Sundar.Repository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sundar.BLL
{
    public class UserManagerApp : BaseApp<Sys_User>
    {
        public RevelanceManagerBLL ReleManagerApp { get; set; }

        public Sys_User Get(string account)
        {
            return Repository.FindSingle(u => u.Account == account);
        }

        /// <summary>
        /// 加载当前登录用户可访问的一个部门及子部门全部用户
        /// </summary>
        public TableData Load(QueryUserListReq request)
        {
            var loginUser = AuthUtil.GetCurrentUser();

            string cascadeId = ".0.";
            if (request.orgId > 0)
            {
                var org = loginUser.Orgs.SingleOrDefault(u => u.Id == request.orgId);
                cascadeId = org.CascadeId;
            }

            var ids = loginUser.Orgs.Where(u => u.CascadeId.Contains(cascadeId)).Select(u => u.Id).ToArray();
            var userIds = ReleManagerApp.Get(Define.USERORG, false, ids);

            var users = UnitWork.Find<Sys_User>(u => userIds.Contains(u.Id))
                   .OrderBy(u => u.Name)
                   .Skip((request.page - 1) * request.limit)
                   .Take(request.limit);

            var records = Repository.GetCount(u => userIds.Contains(u.Id));


            var userviews = new List<UserView>();
            foreach (var user in users.ToList())
            {
                UserView uv = user;
                var orgs = LoadByUser(user.Id);
                uv.Organizations = string.Join(",", orgs.Select(u => u.Name).ToList());
                uv.OrganizationIds = string.Join(",", orgs.Select(u => u.Id).ToList());
                userviews.Add(uv);
            }

            return new TableData
            {
                count = records,
                data = userviews,
            };
        }

        public void AddOrUpdate(UserView view)
        {
            if (string.IsNullOrEmpty(view.OrganizationIds))
                throw new Exception("请为用户分配机构");
            Sys_User user = view;
            if (view.Id <= 0)
            {
                if (UnitWork.IsExist<Sys_User>(u => u.Account == view.Account))
                {
                    throw new Exception("用户账号已存在");
                }
                user.CreateTime = DateTime.Now;
                user.Password = user.Account; //初始密码与账号相同
                Repository.Add(user);
                view.Id = user.Id;   //要把保存后的ID存入view
            }
            else
            {
                UnitWork.Update<Sys_User>(u => u.Id == view.Id, u => new Sys_User
                {
                    Account = user.Account,
                    BizCode = user.BizCode,
                    Name = user.Name,
                    Sex = user.Sex,
                    Status = user.Status
                });
            }
            int[] orgIds = Array.ConvertAll(view.OrganizationIds.Split(',').ToArray(), int.Parse);

            ReleManagerApp.DeleteBy(Define.USERORG, user.Id);
            ReleManagerApp.AddRelevance(Define.USERORG, orgIds.ToLookup(u => user.Id));
        }

        /// <summary>
        /// 加载用户的所有机构
        /// </summary>
        public IEnumerable<Sys_Org> LoadByUser(int userId)
        {
            var result = from userorg in UnitWork.Find<Relevance>(null)
                         join org in UnitWork.Find<Sys_Org>(null) on userorg.SecondId equals org.Id
                         where userorg.FirstId == userId && userorg.Key == Define.USERORG
                         select org;
            return result;
        }


    }
}
