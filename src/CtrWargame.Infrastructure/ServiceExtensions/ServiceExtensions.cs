using CtrWargame.Infrastructure.Persistence.ServiceExtensions;
using CtrWargame.Infrastructure.Services.JsonStaticDataService.ServiceExtensions;
using CtrWargame.Infrastructure.Services.Messaging.ServiceExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CtrWargame.Infrastructure.ServiceExtensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddMessaging(configuration);
        services.AddJsonStaticDataService(configuration);

        return services;
    }
}