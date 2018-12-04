using Sundar.BLL;
using Sundar.Common;
using Sundar.Repository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sundar.MVC.Controllers
{
    public class OrgManagerController : BaseController
    {
        public OrgManagerBLL OrgBLL { get; set; }

        /// <summary>
        /// 部门管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        //添加组织提交
        [HttpPost]
        public string Add(Sys_Org org)
        {
            try
            {
                OrgBLL.Add(org);
            }
            catch (Exception ex)
            {
                Result.Code = -1;
                Result.Message = ex.Message;
            }
            return JsonHelper.Instance.Serialize(Result);
        }
    }
}