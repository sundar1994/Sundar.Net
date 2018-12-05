using Sundar.BLL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sundar.BLL.SSO
{
    public class AuthUtil
    {
        public AuthorizeApp AuthorizeApp { get; set; }

        public AuthUtil()
        {
            AuthorizeApp= AutofacExt.GetFromFac<AuthorizeApp>();
        }

        /// <summary>
        /// 获取当前登录的用户信息
        /// <para>通过URL中的Token参数或Cookie中的Token</para>
        /// </summary>
        /// <param name="remark">The remark.</param>
        /// <returns>LoginUserVM.</returns>
        public UserWithAccessedCtrls GetCurrentUser(string remark = "")
        {

           // var requestUri = String.Format("/api/Check/GetUser?token={0}&requestid={1}", GetToken(), remark);

            try
            {
                // var value = _helper.Get(null, requestUri);
                // var result = JsonHelper.Instance.Deserialize<Response<UserWithAccessedCtrls>>(value);
                UserWithAccessedCtrls result = AuthorizeApp.GetAccessedControls("System");
                return result;
                //if (result.Code == 200)
                //{
                //    return result.Result;
                //}
                //throw new Exception(result.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserWithAccessedCtrls GetUser(string token, string requestid = "")
        {
            var result = new UserWithAccessedCtrls();
            try
            {
                //var user = _objCacheProvider.GetCache(token);
                //if (user != null)
                //{
                //    result.Result = _app.GetAccessedControls(user.Account);
                //}
            }
            catch (Exception ex)
            {
                //result.Code = 500;
                //result.Message = ex.InnerException != null
                //    ? "OpenAuth.WebAPI数据库访问失败:" + ex.InnerException.Message
                //    : "OpenAuth.WebAPI数据库访问失败:" + ex.Message;
            }

            return result;

        }
    }
}
