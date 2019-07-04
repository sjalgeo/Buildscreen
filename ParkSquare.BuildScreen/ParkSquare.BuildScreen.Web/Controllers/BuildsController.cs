using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkSquare.BuildScreen.Web.Models;
using ParkSquare.BuildScreen.Web.Services;

namespace ParkSquare.BuildScreen.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildsController : ControllerBase
    {
        private readonly IViewAggregator _viewAggregator;

        public BuildsController(IViewAggregator viewAggregator)
        {
            _viewAggregator = viewAggregator ?? throw new ArgumentNullException(nameof(viewAggregator));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<BuildInfoDto>>> GetAsync()
        {
            var builds = await _viewAggregator.GetViewAsync();
            return Ok(builds);
        }

        [HttpGet("{sinceHoursAgo}")]
        public async Task<ActionResult<IReadOnlyCollection<BuildInfoDto>>> GetAsync(int sinceHoursAgo)
        {
            var builds = await _viewAggregator.GetViewAsync(sinceHoursAgo);
            return Ok(builds);
        }
    }
}
