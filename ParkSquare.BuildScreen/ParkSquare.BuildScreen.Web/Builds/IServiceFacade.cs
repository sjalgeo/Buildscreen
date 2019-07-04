using System.Collections.Generic;
using ParkSquare.BuildScreen.Web.Models;

namespace ParkSquare.BuildScreen.Web.Builds
{
    public interface IServiceFacade
    {
        IReadOnlyCollection<BuildInfoDto> GetBuilds();
    }
}
