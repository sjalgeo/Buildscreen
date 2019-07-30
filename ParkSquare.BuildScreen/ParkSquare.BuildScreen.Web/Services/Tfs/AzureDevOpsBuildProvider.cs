using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParkSquare.BuildScreen.Web.Services.Tfs.Dto;

namespace ParkSquare.BuildScreen.Web.Services.Tfs
{
    public class AzureDevOpsBuildProvider : IBuildProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IBuildDtoConverter _buildDtoConverter;

        public AzureDevOpsBuildProvider(IHttpClientFactory httpClientFactory, IBuildDtoConverter buildDtoConverter)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _buildDtoConverter = buildDtoConverter ?? throw new ArgumentNullException(nameof(buildDtoConverter));
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
                var validBuilds = new List<BuildDto>();

                foreach (var path in projects)
                {
                    var requestPath = GetRequestPath(path, since);
                    var requestUri = new Uri(new Uri("https://dev.azure.com/parksq/"), requestPath);

                    var response = await client.GetAsync(requestUri);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = response.Content.ReadAsStringAsync().Result;
                        var deserialized = JsonConvert.DeserializeObject<GetBuildsResponseDto>(json);

                        validBuilds.AddRange(deserialized.Value);
                    }
                    else
                    {
                        var x = 1;
                    }
                }

                var converted = new List<Build>();

                // Need to only return the latest one per build definition here

                foreach (var build in validBuilds)
                {
                    converted.Add(_buildDtoConverter.Convert(build));
                }

                return converted;
            }
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