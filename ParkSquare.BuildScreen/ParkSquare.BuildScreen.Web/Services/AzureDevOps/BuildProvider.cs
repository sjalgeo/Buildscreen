using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParkSquare.BuildScreen.Web.Services.AzureDevOps.Dto;

namespace ParkSquare.BuildScreen.Web.Services.AzureDevOps
{
    public class BuildProvider : IBuildProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IBuildDtoConverter _buildDtoConverter;
        private readonly ILatestBuildsFilter _latestBuildsFilter;
        private readonly ITestResultsProvider _testResultsProvider;

        public BuildProvider(
            IHttpClientFactory httpClientFactory, 
            IBuildDtoConverter buildDtoConverter,
            ILatestBuildsFilter buildFilter,
            ITestResultsProvider testResultsProvider)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _buildDtoConverter = buildDtoConverter ?? throw new ArgumentNullException(nameof(buildDtoConverter));
            _latestBuildsFilter = buildFilter ?? throw new ArgumentNullException(nameof(buildFilter));
            _testResultsProvider = testResultsProvider ?? throw new ArgumentNullException(nameof(testResultsProvider));
        }

        public Task<IReadOnlyCollection<Build>> GetBuildsAsync()
        {
            return GetBuildsAsync(new[] {"parksq"}, DateTime.Now.AddDays(-30));
        }

        public Task<IReadOnlyCollection<Build>> GetBuildsAsync(int sinceHours)
        {
            return GetBuildsAsync(new[] { "parksq" }, DateTime.Now.AddHours(-sinceHours));
        }

        private async Task<IReadOnlyCollection<Build>> GetBuildsAsync(IEnumerable<string> projects, DateTime since)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var dtos = new List<BuildDto>();

                foreach (var project in projects)
                {
                    var requestPath = GetRequestPath(project, since);
                    var requestUri = new Uri(new Uri("https://dev.azure.com/parksq/"), requestPath);

                    var response = await client.GetAsync(requestUri);

                    if (response.IsSuccessStatusCode)
                    {
                        var deserialized = await DeserializeResponseAsync(response);
                        dtos.AddRange(deserialized.Value);
                    }
                }

                var latestBuilds = _latestBuildsFilter.GetLatestBuilds(dtos);

                var converted = new List<Build>();

                foreach (var buildDto in latestBuilds)
                {
                    var testResults = await _testResultsProvider.GetTestsForBuildAsync(buildDto.Project.Name, buildDto.Uri);
                    converted.Add(_buildDtoConverter.Convert(buildDto, testResults));
                }

                return converted;
            }
        }

        private static async Task<GetBuildsResponseDto> DeserializeResponseAsync(HttpResponseMessage response)
        {
            var deserialized = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GetBuildsResponseDto>(deserialized);
        }

        private static string GetRequestPath(string project, DateTime buildSince)
        {
            // Should use X-MS-ContinuationToken instead of an arbitrarily large number of results, but
            // on inspecting the responses from TFS, said header doesn't seem to be present. Despite it 
            // being mentioned in Access-Control-Expose-Headers.
            // https://www.visualstudio.com/en-us/docs/integrate/api/build/builds 

            return project + "/_apis/build/builds" + CreateFilterQuery(buildSince) + "&$top=5000";
        }

        private static string CreateFilterQuery(DateTime dateTime)
        {
            // For API versions see:
            // https://docs.microsoft.com/en-us/rest/api/azure/devops/?view=azure-devops-server-rest-5.0#api-and-tfs-version-mapping
            return $"?api-version=4.1&minTime={dateTime:yyyy-MM-ddTHH:mm:ss.000Z}&statusFilter=all&queryOrder=queueTimeAscending";
        }
    }
}