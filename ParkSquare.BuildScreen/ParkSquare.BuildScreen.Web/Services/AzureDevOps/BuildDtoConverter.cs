using ParkSquare.BuildScreen.Web.Services.AzureDevOps.Dto;

namespace ParkSquare.BuildScreen.Web.Services.AzureDevOps
{
    public class BuildDtoConverter : IBuildDtoConverter
    {
        public Build Convert(BuildDto buildDto, TestResults testResults)
        {
            return new Build
            {
                Id = buildDto.Id,
                BuildReportUrl = buildDto.Links.Web.Href,
                RequestedByName = buildDto.RequestedFor.DisplayName,
                Status = string.IsNullOrEmpty(buildDto.Result) ? buildDto.Status : buildDto.Result,
                TotalNumberOfTests = testResults.TotalTests,
                PassedNumberOfTests = testResults.PassedTests,
                TeamProject = buildDto.Project.Name,
                TeamProjectCollection = "xyz collection",
                BuildDefinition = buildDto.Definition.Name,
                StartBuildDateTime = buildDto.StartTime,
                FinishBuildDateTime = buildDto.FinishTime,
                // RequestedByPictureUrl = "https://dev.azure.com/parksq/parksq/_api/_common/identityImage?id=" + buildDto.RequestedBy.Id,
                RequestedByPictureUrl = "https://en.gravatar.com/userimage/64673125/c79a1ab9205094f6fc0937557ae3fde8.jpg",
                Branch = buildDto.SourceBranch
            };
        }

    }
}
