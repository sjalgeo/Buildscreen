using System.Collections.Generic;
using ParkSquare.BuildScreen.Core;

namespace ParkSquare.BuildScreen.Api.Builds
{
    public interface IService
    {
        List<BuildInfoDto> GetBuildInfoDtos();

        List<BuildInfoDto> GetBuildInfoDtosPolling(string filterDate);
    }
}