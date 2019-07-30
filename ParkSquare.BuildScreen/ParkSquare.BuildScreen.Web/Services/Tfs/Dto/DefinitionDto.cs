using Newtonsoft.Json;

namespace ParkSquare.BuildScreen.Web.Services.Tfs.Dto
{
    public class DefinitionDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Url { get; set; }
    }
}