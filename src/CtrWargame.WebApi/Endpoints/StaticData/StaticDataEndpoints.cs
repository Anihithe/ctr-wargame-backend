using CtrWargame.Application.Common.Messaging;
using CtrWargame.Application.Features.Queries;

namespace CtrWargame.WebApi.Endpoints.StaticData;

public class StaticDataEndpoints : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api");
        group.MapGet("/factions", async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var response = await mediator.SendAsync(new GetFactionsQuery(), cancellationToken);
            return Results.Ok(response);
        });
        
        group.MapGet("/allegiances", async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var response = await mediator.SendAsync(new GetAllegiancesQuery(), cancellationToken);
            return Results.Ok(response);
        });
    }
}