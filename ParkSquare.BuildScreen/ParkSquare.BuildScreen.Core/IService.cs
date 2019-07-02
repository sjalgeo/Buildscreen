using System.Collections.Generic;

namespace ParkSquare.BuildScreen.Core
{
    public interface IService
    {
        List<BuildInfoDto> GetBuildInfoDtos();

        List<BuildInfoDto> GetBuildInfoDtosPolling(string filterDate);
    }
}