using System.Web.Http;
using WebActivatorEx;
using Swashbuckle.Application;
using System.Reflection;
using Sundar.WebApi;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Sundar.WebApi
{
    /// <summary>
    /// Swagger配置类
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// 注册
        /// </summary>
        public static void Register()
        {
            var _thisAssembly = typeof(SwaggerConfig).Assembly;
            var _project = MethodBase.GetCurrentMethod().DeclaringType.Namespace;//项目命名空间
            var _xmlPath = string.Format("{0}/bin/{1}.XML", System.AppDomain.CurrentDomain.BaseDirectory, _project);
            var _jsPath = string.Format("{0}.Scripts.swaggerui.swagger_lang.js", _project);

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "API接口文档");
                        c.IncludeXmlComments(_xmlPath);
                        c.CustomProvider((defaultProvider) => new CachingSwaggerProvider(defaultProvider, _xmlPath));
                    })
                .EnableSwaggerUi(c =>
                    {
                        //扩展js  路径规则:项目命名空间.文件夹名称.js文件名称
                        c.InjectJavaScript(_thisAssembly, _jsPath);
                        //默认显示列表
                        c.DocExpansion(DocExpansion.List);
                    });
        }
    }
}
