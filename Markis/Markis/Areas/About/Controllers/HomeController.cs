using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Markis.Areas.About.Controllers
{
    [Area("About")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
