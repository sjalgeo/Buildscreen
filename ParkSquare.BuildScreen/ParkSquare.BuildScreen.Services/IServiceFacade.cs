using System.Collections.Generic;
using ParkSquare.BuildScreen.Models;

namespace ParkSquare.BuildScreen.Services
{
    public interface IServiceFacade
    {
        List<BuildInfoDto> GetBuilds(string dateString = null);
    }
}
