using System.Threading.Tasks;

using deepamour.lib.core.Predictors.WarriorsPredictor;

using Microsoft.AspNetCore.Mvc;

namespace deepamour.web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() => View();

        public async Task<ActionResult> RunModel()
        {
            var predictor = new WarriorsPredictor();

            var result = await predictor.RunPredictorAsync("");

            return View("Results", result.Value);
        }
    }
}