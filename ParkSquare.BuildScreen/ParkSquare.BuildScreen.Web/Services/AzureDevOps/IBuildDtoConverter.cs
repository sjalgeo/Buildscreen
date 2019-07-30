using ParkSquare.BuildScreen.Web.Services.AzureDevOps.Dto;

namespace ParkSquare.BuildScreen.Web.Services.AzureDevOps
{
    public interface IBuildDtoConverter
    {
        Build Convert(BuildDto build, TestResults testResultsDto);
    }
}