using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;

using Autofac;
using Autofac.Integration.WebApi;

using Anger_API.API.Filters;

namespace Anger_API
{
    public static class DIResolver
    {
        public static IContainer Container { get; set; }
        public static void Initialize(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.Name.EndsWith("Repository")
                || t.Name.EndsWith("Service")
                || t.Name.EndsWith("Factory")
                || t.Name.EndsWith("Storage"))
                .AsImplementedInterfaces();



            #region Web Api integrateion
            builder.RegisterType<ApiKeyAuthorizeFilter>()
                .AsWebApiAuthenticationFilterFor<IHttpController>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
            #endregion

            Container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }
    }
}