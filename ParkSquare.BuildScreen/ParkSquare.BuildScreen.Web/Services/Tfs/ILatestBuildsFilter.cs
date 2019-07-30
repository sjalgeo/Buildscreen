using System.Collections.Generic;
using ParkSquare.BuildScreen.Web.Services.Tfs.Dto;

namespace ParkSquare.BuildScreen.Web.Services.Tfs
{
    public interface ILatestBuildsFilter
    {
        List<BuildDto> GetLatestBuilds(List<BuildDto> builds);
    }
}
