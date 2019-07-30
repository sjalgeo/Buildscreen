using System;

namespace ParkSquare.BuildScreen.Web.Services.AzureDevOps
{
    public interface IConfig
    {
        string AuthToken { get; set; }

        int TimeoutSeconds { get; set; }

        string[] Projects { get; set; }

        int MaxBuildAgeDays { get; set; }

        string ServerUrl { get; set; }

        string ProjectCollection { get; set; }

        Uri ApiBaseUrl { get; }
    }
}