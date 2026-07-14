using CtrWargame.Infrastructure.ServiceExtensions;

namespace CtrWargame.WebApi.ServiceExtensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        return services;
    }
}