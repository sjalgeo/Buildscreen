using ParkSquare.BuildScreen.Web.Services.Tfs.Dto;

namespace ParkSquare.BuildScreen.Web.Services.Tfs
{
    public class BuildDtoConverter : IBuildDtoConverter
    {
        public Build Convert(BuildDto buildDto)
        {
            return new Build
            {
                Id = buildDto.Id,
                BuildReportUrl = buildDto.Links.Web.Href,
                RequestedByName = buildDto.RequestedFor.DisplayName,
                Status = string.IsNullOrEmpty(buildDto.Result) ? buildDto.Status : buildDto.Result,
                // TotalNumberOfTests = 0,
                // PassedNumberOfTests = 0,
                TeamProject = buildDto.Project.Name,
                TeamProjectCollection = "xyz collection",
                Builddefinition = buildDto.Definition.Name,
                StartBuildDateTime = buildDto.StartTime,
                FinishBuildDateTime = buildDto.FinishTime,
                // LastBuildTime = TimeSpan.FromHours(-1),
                RequestedByPictureUrl = "https://en.gravatar.com/userimage/64673125/c79a1ab9205094f6fc0937557ae3fde8.jpg"
            };
        }
    }
}
