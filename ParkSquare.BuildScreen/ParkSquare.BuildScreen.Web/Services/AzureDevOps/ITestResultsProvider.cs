using System.Threading.Tasks;

namespace ParkSquare.BuildScreen.Web.Services.AzureDevOps
{
    public interface ITestResultsProvider
    {
        Task<TestResults> GetTestsForBuildAsync(string project, string buildUri);
    }
}