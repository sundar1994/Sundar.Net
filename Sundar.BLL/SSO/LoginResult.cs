using Sundar.Common;

namespace Sundar.BLL.SSO
{
    public class LoginResult : Response<string>
    {
        public string ReturnUrl;
        public string Token;
    }
}
