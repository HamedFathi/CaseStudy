using FluentValidation;
using HamedStack.CQRS;
using HamedStack.CQRS.FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CaseStudy.Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var commandValidatorsAssemblies = AssemblyExtensions.GetAllAppDomainAssemblies().FindAssembliesWithImplementationsOf(typeof(CommandValidator<>));
        var commandValidatorsAssemblies2 = AssemblyExtensions.GetAllAppDomainAssemblies().FindAssembliesWithImplementationsOf(typeof(CommandValidator<,>));
        var queryValidatorsAssemblies = AssemblyExtensions.GetAllAppDomainAssemblies().FindAssembliesWithImplementationsOf(typeof(QueryValidator<,>));

        var validatorsAssemblies = commandValidatorsAssemblies.Concat(commandValidatorsAssemblies2).Concat(queryValidatorsAssemblies).ToList();
        if (validatorsAssemblies.Any())
        {
            services.AddValidatorsFromAssemblies(validatorsAssemblies);
        }

        var allTypes = new[]
        {
            typeof(IMediator),
            typeof(IQuery<>),
            typeof(ICommand),
            typeof(ICommand<>),
            typeof(IQueryHandler<,>),
            typeof(ICommandHandler<>),
            typeof(ICommandHandler<,>)

        };

        var appDomain = AppDomain.CurrentDomain.GetAssemblies();

        var assemblies1 = AssemblyExtensions.AppDomainContains(allTypes);
        var assemblies2 = appDomain.FindAssembliesWithImplementationsOf(typeof(ICommand));
        var assemblies3 = appDomain.FindAssembliesWithImplementationsOf(typeof(ICommand<>));
        var assemblies4 = appDomain.FindAssembliesWithImplementationsOf(typeof(IQuery<>));

        var assemblies = assemblies1.Concat(assemblies2).Concat(assemblies3).Concat(assemblies4).ToArray();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
        services.AddScoped<ICommandQueryDispatcher, CommandQueryDispatcher>();

        return services;
    }
}