using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sundar.WebApi;
using Sundar.WebApi.Controllers;

namespace Sundar.WebApi.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // 排列
            HomeController controller = new HomeController();

            // 操作
            ViewResult result = controller.Index() as ViewResult;

            // 断言
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
