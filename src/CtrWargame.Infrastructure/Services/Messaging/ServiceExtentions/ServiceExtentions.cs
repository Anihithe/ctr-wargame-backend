using CtrWargame.Application.Common;
using CtrWargame.Application.Common.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CtrWargame.Infrastructure.Services.Messaging.ServiceExtentions;

public static class ServiceExtentions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMediator, Mediator>();
        var applicationAssembly = typeof(IApplicationMarker).Assembly;

        var handlerTypes = applicationAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .SelectMany(t => t.GetInterfaces(), (t, i) => new { Implementation = t, Interface = i })
            .Where(x => x.Interface.IsGenericType &&
                        x.Interface.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

        foreach (var handler in handlerTypes)
        {
            services.AddTransient(handler.Interface, handler.Implementation);
        }

        return services;
    }
}