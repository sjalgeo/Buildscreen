using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using ParkSquare.BuildScreen.Web.Models;
using ParkSquare.Testing.Generators;

namespace ParkSquare.BuildScreen.Web.Services
{
    public class AzureDevOpsBuildProvider : IBuildProvider
    {
        public Task<IReadOnlyCollection<Build>> GetBuildsAsync()
        {
            return Task.FromResult((IReadOnlyCollection<Build>) EnumerableGenerator
                .CreateSequenceOfRandomSize(30, DummyBuild).ToList());
        }

        public Task<IReadOnlyCollection<Build>> GetBuildsAsync(int sinceHours)
        {
            return Task.FromResult((IReadOnlyCollection<Build>) EnumerableGenerator
                .CreateSequenceOfRandomSize(5, DummyBuild).ToList());
        }

        private static Build DummyBuild()
        {
            var status = EnumGenerator.AnyEnumValue<BuildStatus>().ToString();

            return new Build
            {
                Id = IntegerGenerator.AnyIntegerInRange(1, 50).ToString(),
                RequestedByName = NameGenerator.AnyName(),
                TeamProject = $"{StringGenerator.SequenceOfAlphas(5)} Project",
                Status = status.Camelize(),
                Builddefinition = $"{status} Build",
                TotalNumberOfTests = IntegerGenerator.AnyIntegerInRange(50, 100),
                TeamProjectCollection = $"{StringGenerator.SequenceOfAlphas(5)} Collection",
                LastBuildTime = DateTimeGenerator.AnyDateTimeBefore(DateTime.Now).TimeOfDay,
                FinishBuildDateTime = DateTimeGenerator.AnyDateBefore(DateTime.Now),
                PassedNumberOfTests = IntegerGenerator.AnyIntegerInRange(0, 50),
                StartBuildDateTime = DateTimeGenerator.AnyDateTimeBefore(DateTime.Now)
            };
        }
    }
}