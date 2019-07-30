using System;
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
                BuildDefinition = buildDto.Definition.Name,
                StartBuildDateTime = buildDto.StartTime,
                FinishBuildDateTime = buildDto.FinishTime,
                RequestedByPictureUrl = string.Empty,
                Branch = ConvertBranchName(buildDto.SourceBranch)
            };
        }

        private static string ConvertBranchName(string branchName)
        {
            return branchName.Replace("refs/", string.Empty, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
