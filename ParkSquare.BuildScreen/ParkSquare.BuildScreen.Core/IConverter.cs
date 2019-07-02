using System.Collections.Generic;

namespace ParkSquare.BuildScreen.Core
{
    public interface IConverter<U> 
    {
        List<BuildInfoDto> Convert(U result);
    }
}