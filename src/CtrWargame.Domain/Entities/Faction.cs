using CtrWargame.Domain.ValueObjects;

namespace CtrWargame.Domain.Entities;

public class Faction(
    int id,
    string name,
    IReadOnlyDictionary<int, Characteristics> baseProfiles,
    string specialRules,
    IReadOnlyCollection<Origin> origins)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public IReadOnlyDictionary<int, Characteristics> BaseProfiles { get; } = baseProfiles;
    public string SpecialRules { get; } = specialRules;
    public IReadOnlyCollection<Origin> Origins { get; } = origins;
}