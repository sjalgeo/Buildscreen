using System;
using System.Collections.Generic;

namespace ParkSquare.BuildScreen.Core
{
    public class ServiceFacade : IServiceFacade
    {
        public List<BuildInfoDto> GetBuilds()
        {
            return new List<BuildInfoDto>
            {
                new BuildInfoDto
                {
                    Id = "JJJJ",
                    RequestedByName = "Jon Meesam",
                    TeamProject = "Team Project",
                    Status = "inProgress",
                    Builddefinition = "Definition",
                    TotalNumberOfTests = 123,
                    TeamProjectCollection = "Collection",
                    LastBuildTime = TimeSpan.FromHours(3),
                    FinishBuildDateTime = DateTime.Now,
                    PassedNumberOfTests = 100,
                    RequestedByPictureUrl = "",
                    StartBuildDateTime = DateTime.Now.AddMinutes(-20)
                }
            };
        }
    }
}