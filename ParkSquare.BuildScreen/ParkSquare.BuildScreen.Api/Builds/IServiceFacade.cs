using System.Collections.Generic;
using ParkSquare.BuildScreen.Core;

namespace ParkSquare.BuildScreen.Api.Builds
{
    public interface IServiceFacade
    {
        IReadOnlyCollection<BuildInfoDto> GetBuilds();
    }
}
