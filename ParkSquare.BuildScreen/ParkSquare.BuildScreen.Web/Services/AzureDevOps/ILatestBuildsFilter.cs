using System.Collections.Generic;
using ParkSquare.BuildScreen.Web.Services.AzureDevOps.Dto;

namespace ParkSquare.BuildScreen.Web.Services.AzureDevOps
{
    public interface ILatestBuildsFilter
    {
        List<BuildDto> GetLatestBuilds(List<BuildDto> builds);
    }
}
