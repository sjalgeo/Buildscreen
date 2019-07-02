using System.Web.Mvc;
using ParkSquare.BuildScreen.Core;

namespace ParkSquare.BuildScreen.Web.Controllers
{
    public class BuildscreenApiController : Controller
    {

        private readonly IServiceFacade _serviceFacade;

        public BuildscreenApiController(IServiceFacade serviceFacade)
        {
            _serviceFacade = serviceFacade;
        }

        [OutputCache(CacheProfile = "CacheDuration")]
        public ActionResult GetBuilds()
        {
            var allBuilds = _serviceFacade.GetBuilds();

            return Json(allBuilds, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(CacheProfile = "CacheDuration", VaryByParam = "*")]
        public ActionResult GetBuildsSince(string dateString)
        {
            var allBuilds = _serviceFacade.GetBuilds();
            return Json(allBuilds, JsonRequestBehavior.AllowGet);
        }
    }
}