namespace UriShortening.BusinessLogic.AutofacConfiguration
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Autofac;
    using Autofac.Builder;
    using Autofac.Features.Scanning;
    using AutoMapper;
    using Validation;
    using Persistence.Configuration;

    public static class AutofacExtensions
    {
        public static IRegistrationBuilder<TService, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterServicePerLifetimeScope<TService>(this ContainerBuilder builder)
        {
            return builder.RegisterType<TService>()
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerLifetimeScope();
        }

        public static IRegistrationBuilder<TDbContext, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterDbContextPerLifeTimeScope<TDbContext>(this ContainerBuilder builder)
        {
            return builder.RegisterType<TDbContext>()
                .AsImplementedInterfaces()
                .AsSelf()
                .WithParameter(new TypedParameter(typeof(string), ConnectionStringReader.GetConnectionString(typeof(TDbContext).Name)))
                .InstancePerLifetimeScope();
        }

        public static ContainerBuilder UseAutoMapper(this ContainerBuilder containerBuilder, Assembly assembly)
        {
            containerBuilder.Register(context =>
                {
                    var profiles = context.Resolve<IEnumerable<Profile>>();
                    var configuration = new MapperConfiguration(cfg =>
                    {
                        foreach (var profile in profiles)
                        {
                            cfg.AddProfile(profile);
                        }
                    });
                    return configuration.CreateMapper();
                })
                .As<IMapper>()
                .SingleInstance();

            containerBuilder
                .RegisterAssemblyInheritedType<Profile>(assembly)
                .As<Profile>();
            return containerBuilder;
        }

        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterAssemblyInheritedType<TBaseClass>(
            this ContainerBuilder containerBuilder,
            Assembly assembly = null)
        {
            assembly = assembly ?? Assembly.GetCallingAssembly();
            return containerBuilder.RegisterAssemblyTypes(assembly)
                .Where(t => typeof(TBaseClass).IsAssignableFrom(t));
        }

        public static ContainerBuilder UseValidation(this ContainerBuilder containerBuilder, Assembly assembly)
        {
            containerBuilder.RegisterType<BusinessRulesValidator>()
                .As<IValidator>()
                .SingleInstance();

            containerBuilder.RegisterAssemblyTypes(assembly)
                .Where(t => typeof(FluentValidation.IValidator).IsAssignableFrom(t) && !t.IsAbstract && t.GetConstructor(Type.EmptyTypes) != null)
                .As<FluentValidation.IValidator>();

            return containerBuilder;
        }
    }
}
