using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ParkSquare.BuildScreen.Api.Builds;
using ParkSquare.BuildScreen.Core;

namespace ParkSquare.BuildScreen.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildsController : ControllerBase
    {
        private readonly IServiceFacade _serviceFacade;

        public BuildsController(IServiceFacade serviceFacade)
        {
            _serviceFacade = serviceFacade;
        }

        [HttpGet]
        public ActionResult<IReadOnlyCollection<BuildInfoDto>> Get()
        {
            return Ok(_serviceFacade.GetBuilds());
        }

        [HttpGet("{since}")]
        public ActionResult<IReadOnlyCollection<BuildInfoDto>> Get(string dateString)
        {
            return Ok(_serviceFacade.GetBuilds());
        }
    }
}
