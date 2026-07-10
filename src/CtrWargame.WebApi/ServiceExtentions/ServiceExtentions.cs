using CtrWargame.Infrastructure.ServiceExtensions;

namespace CtrWargame.WebApi.ServiceExtentions;

public static class ServiceExtentions
{
    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        return services;
    }
}