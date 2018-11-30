using System.Web.Mvc;

namespace Sundar.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/Swagger/ui/index");
        }
    }
}
