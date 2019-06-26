using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using OrbitOne.BuildScreen.Configuration;
using OrbitOne.BuildScreen.RestApiService;
using OrbitOne.BuildScreen.Services;

namespace OrbitOne.BuildScreen.DependencyInjection
{
    public class BuildScreenRegistry : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IServiceFacade>().ImplementedBy<ServiceFacade>(),
                Component.For<IConfigurationRestService>().Instance(ConfigurationRestService.Load()),
                Component.For<IServiceConfig>().ImplementedBy<ServiceConfig>());

            var configs = ServiceConfiguration.GetListOfConfigurationsInternal();
            int i = 0;
            foreach (var config in configs)
            {
                i++;
                switch (config.ServiceType)
                {
                    case "VSO":
                        container.Register(
                            Component.For<IHelperClass>()
                                .ImplementedBy<VsoHelperClass>()
                                .DependsOn(Dependency.OnValue<IServiceConfig>(config)).Named("HelperClass" + i));
                        var instanceVso = container.Resolve<IHelperClass>("HelperClass" + i);
                        container.Register(
                            Component.For<IService>().ImplementedBy<VsoRestService>().DependsOn(Dependency.OnValue<IHelperClass>(instanceVso)).Named("VsoRestService"+i));
                        break;
                    case "TFS":
                        throw new NotImplementedException("No longer supported");
                        break;
                }
            }

        }
    }
}
