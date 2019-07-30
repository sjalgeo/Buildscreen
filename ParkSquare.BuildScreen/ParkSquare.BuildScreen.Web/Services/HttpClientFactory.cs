using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using ParkSquare.BuildScreen.Web.Services.AzureDevOps;

namespace ParkSquare.BuildScreen.Web.Services
{
    public class HttpClientFactory : IHttpClientFactory
    {
        private readonly IConfig _config;

        public HttpClientFactory(IConfig config)
        {
            _config = config;
        }

        public HttpClient CreateClient()
        {
            var client = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip
            });

            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes($":{_config.AuthToken}")));

            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromSeconds(_config.TimeoutSeconds);

            return client;
        }
    }
}
