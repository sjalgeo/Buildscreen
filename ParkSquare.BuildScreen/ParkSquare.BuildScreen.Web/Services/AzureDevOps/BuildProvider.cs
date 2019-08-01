using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IConfig _config;

        public BuildProvider(
            IHttpClientFactory httpClientFactory, 
            IBuildDtoConverter buildDtoConverter,
            ILatestBuildsFilter buildFilter,
            ITestResultsProvider testResultsProvider,
            IConfig config)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _buildDtoConverter = buildDtoConverter ?? throw new ArgumentNullException(nameof(buildDtoConverter));
            _latestBuildsFilter = buildFilter ?? throw new ArgumentNullException(nameof(buildFilter));
            _testResultsProvider = testResultsProvider ?? throw new ArgumentNullException(nameof(testResultsProvider));
            _config = config ?? throw new ArgumentNullException(nameof(config));

        }

        public Task<IReadOnlyCollection<Build>> GetBuildsAsync()
        {
            return GetBuildsAsync(_config.Projects, DateTime.Now.AddDays(-_config.MaxBuildAgeDays));
        }

        public Task<IReadOnlyCollection<Build>> GetBuildsAsync(int sinceHours)
        {
            return GetBuildsAsync(_config.Projects, DateTime.Now.AddHours(-sinceHours));
        }

        private async Task<IReadOnlyCollection<Build>> GetBuildsAsync(IEnumerable<string> projects, DateTime since)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var dtos = new List<BuildDto>();

                foreach (var project in projects)
                {
                    var requestPath = GetRequestPath(project, since);
                    var requestUri = new Uri(_config.ApiBaseUrl, requestPath);

                    var response = await client.GetAsync(requestUri);

                    if (response.IsSuccessStatusCode)
                    {
                        var deserialized = await DeserializeResponseAsync(response);
                        dtos.AddRange(deserialized.Value);
                    }
                }

                var latestBuilds = _latestBuildsFilter.GetLatestBuilds(dtos);

                var getTestTasks =
                    latestBuilds.Select(x =>
                        _testResultsProvider.GetTestsForBuildAsync(x.Project.Name, x.Uri)).ToList();

                await Task.WhenAll(getTestTasks);

                var testResults = getTestTasks.Select(x => x.Result).ToList();

                var converted = new List<Build>();
                foreach (var buildDto in latestBuilds)
                {
                    var testsForBuild = testResults.FirstOrDefault(x => x.BuildUri.Equals(buildDto.Uri));
                    converted.Add(_buildDtoConverter.Convert(buildDto, testsForBuild));
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
            return $"?api-version=5.0&minTime={dateTime:yyyy-MM-ddTHH:mm:ss.000Z}&statusFilter=all&queryOrder=queueTimeAscending";
        }
    }
}