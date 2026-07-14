namespace CtrWargame.Domain.Entities;

public class Allegiance(int id, string type, string name, string description)
{
    public int Id { get; } = id;
    public string Type { get; } = type;
    public string Name { get; } = name;
    public string Description { get; } = description;
}