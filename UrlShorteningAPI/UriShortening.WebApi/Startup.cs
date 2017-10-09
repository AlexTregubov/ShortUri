using UriShortening.WebApi.Controllers;
using UriShortening.WebApi.Filters;

[assembly: Microsoft.Owin.OwinStartup(typeof(UriShortening.WebApi.Startup))]

namespace UriShortening.WebApi
{
    using Owin;
    using Autofac;
    using System.Reflection;
    using System.Web.Http;
    using Autofac.Integration.WebApi;
    using BusinessLogic.AutofacConfiguration;

    public class Startup
    {
        protected ILifetimeScope LifetimeScope { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            var containerBuilder = new ContainerBuilder();

            var container = ConfigureAutofac(containerBuilder).Build();
            Configuration(app, container);

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        public ContainerBuilder ConfigureAutofac(ContainerBuilder builder)
        {
            builder.RegisterModule<BusinessLogicModule>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
            builder.RegisterType<BasicAuthenticationFilter>()
                .AsWebApiAuthorizationFilterOverrideFor<UriController>()
                .InstancePerRequest();

            return builder;
        }

        public virtual void Configuration(IAppBuilder app, ILifetimeScope lifetimeScope)
        {
            var config = new HttpConfiguration();

            LifetimeScope = lifetimeScope;

            UseAutofac(app, config);

            app.UseCors();

            UsePreconfiguredWebApi(app, config);
        }

        private void UseAutofac(IAppBuilder app, HttpConfiguration config)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(LifetimeScope);

            app.UseAutofacMiddleware(LifetimeScope);
            app.UseAutofacWebApi(config);
        }

        private static IAppBuilder UsePreconfiguredWebApi(
            IAppBuilder app,
            HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config = UseJsonFormatter(config);

            return app.UseWebApi(config);
        }

        private static HttpConfiguration UseJsonFormatter(HttpConfiguration config)
        {
            var jsonFormatter = config.Formatters.JsonFormatter;

            config.Formatters.Clear();
            config.Formatters.Add(jsonFormatter);
            return config;
        }
    }
}
