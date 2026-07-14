using CtrWargame.Application.Common.Messaging;
using CtrWargame.Domain.Entities;
using CtrWargame.Domain.ValueObjects;

namespace CtrWargame.Application.Features.Queries;

public record GetFactionsQuery : IQuery<IEnumerable<GetFactionsResponse>>;

public record GetFactionsResponse(
    string Name,
    IReadOnlyDictionary<int, Characteristics> BaseProfiles,
    IReadOnlyCollection<string> SpecialRules,
    IReadOnlyCollection<Origin> Origins)
{
    public static GetFactionsResponse FromDomain(Faction faction)
    {
        return new GetFactionsResponse(faction.Name, faction.BaseProfiles, faction.SpecialRules, faction.Origins);
    }
};

public class GetFactionsQueryHandler(IStaticDataService staticDataService) : IQueryHandler<GetFactionsQuery, IEnumerable<GetFactionsResponse>>
{
    public Task<IEnumerable<GetFactionsResponse>> HandleAsync(GetFactionsQuery query, CancellationToken cancellationToken)
    {
        return Task.FromResult(staticDataService.Factions.Select(GetFactionsResponse.FromDomain));
    }
}