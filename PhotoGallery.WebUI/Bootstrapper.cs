using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using PhotoGallery.DB.Infrastructure;
using PhotoGallery.DB.Infrastructure.Repositories;
using PhotoGallery.DB.Infrastructure.Services;
using PhotoGallery.WebUI.Mappings;

namespace PhotoGallery.WebUI
{
    public class Bootstrapper
    {
        public static void Run(Type mvcType)
        {
            System.Data.Entity.Database.SetInitializer(new DbInitializer());
            SetAutofacContainer(mvcType);
            //Configure AutoMapper
            AutoMapperConfiguration.Configure();

        }
        private static void SetAutofacContainer(Type mvcType)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(mvcType.Assembly);
            builder.RegisterAssemblyTypes(typeof(AlbumRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(MembershipService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();
            
            builder.RegisterFilterProvider();
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
