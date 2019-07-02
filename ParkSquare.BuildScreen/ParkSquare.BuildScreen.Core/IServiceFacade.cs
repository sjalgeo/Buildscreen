using System.Collections.Generic;

namespace ParkSquare.BuildScreen.Core
{
    public interface IServiceFacade
    {
        List<BuildInfoDto> GetBuilds();
    }
}
