using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace ParkSquare.BuildScreen.DependencyInjection
{
    public class ControllerRegistry : IWindsorInstaller
    {
       
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("ParkSquare.BuildScreen")
                                .BasedOn<IController>()
                                .LifestyleTransient());
        }
    }
}
