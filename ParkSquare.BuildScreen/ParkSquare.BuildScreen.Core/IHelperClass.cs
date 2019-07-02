using System;
using System.Threading.Tasks;

namespace ParkSquare.BuildScreen.Core
{
    public interface IHelperClass
    {
        Task<T[]> RetrieveTask<T>(string formattedUrl);
        string ConvertReportUrl(string teamProjectName, string buildUri, Boolean summary);
    }
}