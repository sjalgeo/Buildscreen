using ParkSquare.BuildScreen.Web.Services.Tfs.Dto;

namespace ParkSquare.BuildScreen.Web.Services.Tfs
{
    public interface IBuildDtoConverter
    {
        Build Convert(BuildDto build);
    }
}