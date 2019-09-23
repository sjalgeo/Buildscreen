using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkSquare.BuildScreen.Web.Services;
using ParkSquare.BuildScreen.Web.Services.AzureDevOps;
using Serilog;

namespace ParkSquare.BuildScreen.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IBuildProvider, BuildProvider>();
            services.AddSingleton<IHttpClientFactory, HttpClientFactory>();
            services.AddSingleton<IBuildDtoConverter, BuildDtoConverter>();
            services.AddSingleton<IBuildFilter, LatestBuildsFilter>();
            services.AddSingleton<IBuildFilter, CompletedPullRequestFilter>();
            services.AddSingleton<ITestResultsProvider, TestResultsProvider>();
            services.AddSingleton<IConfig, Config>();
            services.AddSingleton<IBranchNameConverter, BranchNameConverter>();
            services.AddSingleton<IDisplayTransformer, DisplayTransformer>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSerilogRequestLogging(); // replaces noisy built-in version
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
