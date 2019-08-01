using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParkSquare.BuildScreen.Web.Models;
using ParkSquare.BuildScreen.Web.Services;

namespace ParkSquare.BuildScreen.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBuildProvider _buildProvider;

        public BuildsController(IMapper mapper, IBuildProvider buildProvider)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _buildProvider = buildProvider ?? throw new ArgumentNullException(nameof(buildProvider));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<BuildInfoDto>>> GetAsync()
        {
            var builds = await _buildProvider.GetBuildsAsync();
            return Ok(_mapper.Map<IReadOnlyCollection<BuildInfoDto>>(builds));
        }

        [HttpGet("{sinceHoursAgo}")]
        public async Task<ActionResult<IReadOnlyCollection<BuildInfoDto>>> GetAsync(int sinceHoursAgo)
        {
            var builds = await _buildProvider.GetBuildsAsync(sinceHoursAgo);
            return Ok(_mapper.Map<IReadOnlyCollection<BuildInfoDto>>(builds));
        }
    }
}
