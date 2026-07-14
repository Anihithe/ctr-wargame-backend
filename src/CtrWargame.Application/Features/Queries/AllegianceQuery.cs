using CtrWargame.Application.Common.Messaging;
using CtrWargame.Domain.Entities;

namespace CtrWargame.Application.Features.Queries;

public record GetAllegiancesQuery : IQuery<IEnumerable<GetAllegiancesResponse>>;

public record GetAllegiancesResponse(string Type, string Name, string Description)
{
    public static GetAllegiancesResponse FromDomain(Allegiance allegiance)
    {
        return new GetAllegiancesResponse(allegiance.Type, allegiance.Name, allegiance.Description);
    }
}

public class GetAllegianceQueryHandler(IStaticDataService staticDataService) : IQueryHandler<GetAllegiancesQuery, IEnumerable<GetAllegiancesResponse>>
{
    public Task<IEnumerable<GetAllegiancesResponse>> HandleAsync(GetAllegiancesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(staticDataService.Allegiances.Select(GetAllegiancesResponse.FromDomain));
    }
}