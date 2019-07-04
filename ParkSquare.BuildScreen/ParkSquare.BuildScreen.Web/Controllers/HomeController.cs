using Microsoft.AspNetCore.Mvc;

namespace ParkSquare.BuildScreen.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
