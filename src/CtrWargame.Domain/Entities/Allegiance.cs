namespace CtrWargame.Domain.Entities;

public class Allegiance(int id, string name, string bonusName, string description)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public string BonusName { get; } = bonusName;
    public string Description { get; } = description;
}