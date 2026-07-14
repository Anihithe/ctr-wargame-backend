using CtrWargame.Application.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CtrWargame.Infrastructure.Services.JsonStaticDataService.ServiceExtensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddJsonStaticDataService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IStaticDataService, JsonStaticDataService>();

        return services;
    }
}