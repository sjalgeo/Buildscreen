using System.Collections.Generic;
using System.Threading.Tasks;
using ParkSquare.BuildScreen.Web.Models;

namespace ParkSquare.BuildScreen.Web.Services
{
    public interface IViewAggregator
    {
        Task<IReadOnlyCollection<BuildInfoDto>> GetViewAsync();

        Task<IReadOnlyCollection<BuildInfoDto>> GetViewAsync(int sinceHours);
    }
}