using System.Collections.Generic;
using ParkSquare.BuildScreen.Web.Services.AzureDevOps.Dto;

namespace ParkSquare.BuildScreen.Web.Services.AzureDevOps
{
    public interface IBuildFilter
    {
        IEnumerable<BuildDto> Filter(IEnumerable<BuildDto> builds);
    }
}
