﻿using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace ParkSquare.BuildScreen.Web
{
    public class ControllerRegistry : IWindsorInstaller
    {
       
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("ParkSquare.BuildScreen.Web")
                                .BasedOn<IController>()
                                .LifestyleTransient());
        }
    }
}