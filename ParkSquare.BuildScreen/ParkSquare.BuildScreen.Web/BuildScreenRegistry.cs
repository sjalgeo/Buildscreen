using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ParkSquare.BuildScreen.Core;

namespace ParkSquare.BuildScreen.Web
{
    public class BuildScreenRegistry : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IServiceFacade>().ImplementedBy<ServiceFacade>());
        }
    }
}
