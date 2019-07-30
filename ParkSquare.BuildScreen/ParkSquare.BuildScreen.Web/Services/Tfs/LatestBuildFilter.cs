using System;
using System.Collections.Generic;
using System.Linq;
using ParkSquare.BuildScreen.Web.Services.Tfs.Dto;

namespace ParkSquare.BuildScreen.Web.Services.Tfs
{
    public class LatestBuildsFilter : ILatestBuildsFilter
    {
        public List<BuildDto> GetLatestBuilds(List<BuildDto> builds)
        {
            var definitions = builds.Select(GetBuildDefinitionKey).Distinct();
            var filtered = new List<BuildDto>(builds.Where(IsQueued));

            foreach (var definition in definitions)
            {
                var latest =
                    builds.Where(x => GetBuildDefinitionKey(x) == definition && !IsQueued(x))
                        .OrderByDescending(x => x.FinishTime)
                        .FirstOrDefault();

                if (latest != null) filtered.Add(latest);
            }

            return filtered;
        }

        private static string GetBuildDefinitionKey(BuildDto buildDto)
        {
            return buildDto.Repository.Type.Equals("TfsGit", StringComparison.InvariantCultureIgnoreCase)
                ? $"{buildDto.Definition.Name}|{buildDto.SourceBranch}"
                : buildDto.Definition.Name;
        }

        private static bool IsQueued(BuildDto buildDto)
        {
            return buildDto.Status.Equals("inProgress", StringComparison.InvariantCultureIgnoreCase) ||
                   buildDto.Status.Equals("notStarted", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}