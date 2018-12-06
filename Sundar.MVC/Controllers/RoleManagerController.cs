using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using Sundar.Common;
using Sundar.BLL;
using Sundar.BLL.Request;
using Sundar.BLL.Response;
using Sundar.MVC.Controllers;
using Sundar.Mvc.Models;

namespace OpenAuth.Mvc.Controllers
{
    public class RoleManagerController : BaseController
    {
        public RoleManagerBLL RoleBLL { get; set; }
        public RevelanceManagerBLL RevelanceManagerBLL { get; set; }

        /// <summary>
        /// 角色管理主页
        /// </summary>
        /// <returns></returns>
        [Authenticate]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Assign()
        {
            return View();
        }

        //添加角色
        [System.Web.Mvc.HttpPost]
        public string Add(RoleView obj)
        {
            try
            {
                RoleBLL.Add(obj);
            }
            catch (Exception ex)
            {
                Result.Code = 500;
                Result.Message = ex.Message;
            }
            return JsonHelper.Instance.Serialize(Result);
        }

        //修改角色
        [System.Web.Mvc.HttpPost]
        public string Update(RoleView obj)
        {
            try
            {
                RoleBLL.Update(obj);

            }
            catch (Exception ex)
            {
                Result.Code = 500;
                Result.Message = ex.Message;
            }
            return JsonHelper.Instance.Serialize(Result);
        }

        /// <summary>
        /// 加载用户的角色
        /// </summary>
        public string LoadForUser(int userId)
        {
            try
            {
                var result = new Response<List<int>>
                {
                    Result = RevelanceManagerBLL.Get(Define.USERROLE, true, userId)
                };
                return JsonHelper.Instance.Serialize(result);
            }
            catch (Exception e)
            {
                Result.Code = 500;
                Result.Message = e.Message;
            }

            return JsonHelper.Instance.Serialize(Result);
        }

        /// <summary>
        /// 加载组织下面的所有用户
        /// </summary>
        public string Load([FromUri]QueryRoleListReq request)
        {
            return JsonHelper.Instance.Serialize(RoleBLL.Load(request));
        }

        [System.Web.Mvc.HttpPost]
        public string Delete(int[] ids)
        {
            try
            {
                RoleBLL.Delete(ids);
            }
            catch (Exception e)
            {
                Result.Code = 500;
                Result.Message = e.Message;
            }

            return JsonHelper.Instance.Serialize(Result);
        }
    }
}