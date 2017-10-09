namespace UriShortening.BusinessLogic.AutofacConfiguration
{
    using System.Reflection;
    using Services;
    using Persistence.Context;

    public class BusinessLogicModule : Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            builder
                .UseAutoMapper(currentAssembly)
                .UseValidation(currentAssembly);

            builder.RegisterDbContextPerLifeTimeScope<ShortUriContext>();
            builder.RegisterServicePerLifetimeScope<ShortUriService>();
        }
    }
}
