using System.Collections.Generic;
using ParkSquare.BuildScreen.Models;

namespace ParkSquare.BuildScreen.Services
{
    public interface IService
    {
        List<BuildInfoDto> GetBuildInfoDtos();
        List<BuildInfoDto> GetBuildInfoDtosPolling(string filterDate);
    }
}