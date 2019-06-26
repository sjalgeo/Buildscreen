using System.Collections.Generic;
using ParkSquare.BuildScreen.Models;

namespace ParkSquare.BuildScreen.Converters
{
    public interface IConverter<U> 
    {
        List<BuildInfoDto> Convert(U result);
    }
}