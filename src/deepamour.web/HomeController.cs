using Microsoft.AspNetCore.Mvc;

namespace deepamour.web
{
    public class HomeController : Controller
    {
        public ActionResult Index() => View();
    }
}