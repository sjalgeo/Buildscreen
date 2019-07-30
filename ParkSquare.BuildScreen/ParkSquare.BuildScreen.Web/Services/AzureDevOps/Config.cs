using System;
using Microsoft.Extensions.Configuration;

namespace ParkSquare.BuildScreen.Web.Services.AzureDevOps
{
    public class Config : IConfig
    {
        public Config(IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            configuration.Bind("AzureDevOpsProvider", this);

            ApiBaseUrl = GetApiBaseUrl();
        }

        public string AuthToken { get; set; }

        public int TimeoutSeconds { get; set; }

        public string[] Projects { get; set; }

        public int MaxBuildAgeDays { get; set; }

        public string ServerUrl { get; set; }

        public string ProjectCollection { get; set; }

        public Uri ApiBaseUrl { get; }

        private Uri GetApiBaseUrl()
        {
            var separator = ServerUrl.EndsWith('/') ? string.Empty : "/";
            return new Uri($"{ServerUrl}{separator}{ProjectCollection}/");
        }
    }
}