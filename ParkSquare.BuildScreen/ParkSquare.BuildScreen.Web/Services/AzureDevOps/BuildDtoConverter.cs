using System;
using ParkSquare.BuildScreen.Web.Services.AzureDevOps.Dto;

namespace ParkSquare.BuildScreen.Web.Services.AzureDevOps
{
    public class BuildDtoConverter : IBuildDtoConverter
    {
        private readonly IBranchNameConverter _branchNameConverter;

        public BuildDtoConverter(IBranchNameConverter branchNameConverter)
        {
            _branchNameConverter = branchNameConverter ?? throw new ArgumentNullException(nameof(branchNameConverter));
        }

        public Build Convert(BuildDto buildDto, TestResults testResults)
        {
            return new Build
            {
                Id = buildDto.Id,
                BuildReportUrl = buildDto.Links.Web.Href,
                RequestedByName = buildDto.RequestedFor.DisplayName,
                Status = string.IsNullOrEmpty(buildDto.Result) ? buildDto.Status : buildDto.Result,
                TotalNumberOfTests = testResults?.TotalTests ?? 0,
                PassedNumberOfTests = testResults?.PassedTests ?? 0,
                TeamProject = buildDto.Project.Name,
                BuildDefinition = buildDto.Definition.Name,
                StartBuildDateTime = buildDto.StartTime,
                FinishBuildDateTime = buildDto.FinishTime,
                RequestedByPictureUrl = string.Empty,
                Branch = ConvertBranchName(buildDto.SourceBranch),
                RepoName = buildDto.Repository.Name
            };
        }

        private string ConvertBranchName(string branchName)
        {
            return _branchNameConverter.Convert(branchName);
        }
    }
}
