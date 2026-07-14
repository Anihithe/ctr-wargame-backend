using CtrWargame.WebApi.Endpoints.StaticData;

namespace CtrWargame.WebApi.Endpoints;

public static class EndpointExtensions
{
    private static readonly Action<IEndpointRouteBuilder>[] Endpoints = [
        app => new StaticDataEndpoints().MapEndpoints(app)
    ];
    
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        foreach (var map in Endpoints)
        {
            map(app);
        }
        return app;
    }
}