using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkSquare.BuildScreen.Web.Services
{
    public interface IBuildProvider
    {
        Task<IReadOnlyCollection<Build>> GetBuildsAsync();

        Task<IReadOnlyCollection<Build>> GetBuildsAsync(int sinceHours);
    }
}
