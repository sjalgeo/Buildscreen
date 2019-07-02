using System.Web.Mvc;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;

namespace ParkSquare.BuildScreen.Web
{
   public class Bootstrapper
   {
       public static IWindsorContainer Bootstrap()
       {
           var container = new WindsorContainer();
           container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
           container.Install(new ControllerRegistry());
           container.Install(new BuildScreenRegistry());
           var controllerFactory = new WindsorControllerFactory(container.Kernel);
           ControllerBuilder.Current.SetControllerFactory(controllerFactory);           
           return container;
       }
   }
}
