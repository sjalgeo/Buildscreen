using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ParkSquare.BuildScreen.Web.Models;

namespace ParkSquare.BuildScreen.Web.Services
{
    public class ViewAggregator : IViewAggregator
    {
        private readonly IMapper _mapper;
        private readonly List<IBuildProvider> _buildProviders;

        public ViewAggregator(IEnumerable<IBuildProvider> buildProviders, IMapper mapper)
        {
            if (buildProviders == null) throw new ArgumentNullException(nameof(buildProviders));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _buildProviders = buildProviders.ToList();
        }

        public async Task<IReadOnlyCollection<BuildInfoDto>> GetViewAsync()
        {
            var builds = await _buildProviders.First().GetBuildsAsync();
            var dtos = _mapper.Map<IReadOnlyCollection<BuildInfoDto>>(builds);
            return dtos;
        }

        public async Task<IReadOnlyCollection<BuildInfoDto>> GetViewAsync(int sinceHours)
        {
            var builds = await _buildProviders.First().GetBuildsAsync(sinceHours);
            var dtos = _mapper.Map<IReadOnlyCollection<BuildInfoDto>>(builds);
            return dtos;
        }
    }
}
