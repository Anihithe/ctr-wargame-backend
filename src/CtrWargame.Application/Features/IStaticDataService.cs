using CtrWargame.Domain.Entities;

namespace CtrWargame.Application.Features;

public interface IStaticDataService
{
    public IEnumerable<Faction> Factions { get; }
    public IEnumerable<Allegiance> Allegiances { get; }
}