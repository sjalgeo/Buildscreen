using AutoMapper;
using ParkSquare.BuildScreen.Web.Models;

namespace ParkSquare.BuildScreen.Web.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Build, BuildInfoDto>();
        }
    }
}
