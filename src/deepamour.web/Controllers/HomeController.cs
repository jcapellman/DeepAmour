using Microsoft.AspNetCore.Mvc;

namespace deepamour.web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() => View();
    }
}