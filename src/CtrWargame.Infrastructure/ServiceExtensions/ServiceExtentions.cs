using CtrWargame.Infrastructure.Persistence;
using CtrWargame.Infrastructure.Persistence.ServiceExtentions;
using CtrWargame.Infrastructure.Servicies.Messaging.ServiceExtentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CtrWargame.Infrastructure.ServiceExtensions;

public static class ServiceExtentions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddMessaging(configuration);

        return services;
    }
}