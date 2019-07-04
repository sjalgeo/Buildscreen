using System;
using System.Collections.Generic;
using System.Linq;
using Humanizer;
using ParkSquare.BuildScreen.Core;
using ParkSquare.Testing.Generators;

namespace ParkSquare.BuildScreen.Api.Builds
{
    public class ServiceFacade : IServiceFacade
    {
        public IReadOnlyCollection<BuildInfoDto> GetBuilds()
        {
            return EnumerableGenerator.CreateSequenceOfRandomSize(30, DummyBuildInfoDto).ToList();
        }

        private static BuildInfoDto DummyBuildInfoDto()
        {
            var status = EnumGenerator.AnyEnumValue<BuildStatus>().ToString();

            return new BuildInfoDto
            {
                Id = IntegerGenerator.AnyIntegerInRange(1, 50).ToString(),
                RequestedByName = NameGenerator.AnyName(),
                TeamProject = $"{StringGenerator.SequenceOfAlphas(5)} Project",
                Status = status.Camelize(),
                Builddefinition = $"{status} Build",
                TotalNumberOfTests = IntegerGenerator.AnyIntegerInRange(50, 100),
                TeamProjectCollection = $"{StringGenerator.SequenceOfAlphas(5)} Collection",
                LastBuildTime = DateTimeGenerator.AnyDateBefore(DateTime.Now).TimeOfDay,
                FinishBuildDateTime = DateTimeGenerator.AnyDateBefore(DateTime.Now),
                PassedNumberOfTests = IntegerGenerator.AnyIntegerInRange(0, 50),
                RequestedByPictureUrl = "https://en.gravatar.com/userimage/64673125/c79a1ab9205094f6fc0937557ae3fde8.jpg",
                StartBuildDateTime = DateTimeGenerator.AnyDateTimeBefore(DateTime.Now)
            };
        }
    }
}