using System.Collections.Generic;

namespace ParkSquare.BuildScreen.Web.Services.AzureDevOps.Dto
{
    public class GetTestRunsResponseDto
    {
        public int Count { get; set; }

        public IReadOnlyCollection<TestRunDto> Value { get; set; }
    }
}