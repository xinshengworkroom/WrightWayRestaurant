using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Reflection;
using System.Web.Mvc;

namespace WrightWayRestaurant.Web
{
    public class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();


            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterControllers(assembly);
            Type[] rtypes = assembly.GetTypes();
            builder.RegisterTypes(rtypes)
                .AsImplementedInterfaces();

            var servicesAss = Assembly.Load("WrightWayRestaurant.Services");
            Type[] stypes = servicesAss.GetTypes();
            builder.RegisterTypes(stypes)
                .AsImplementedInterfaces();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}