namespace CtrWargame.Domain.ValueObjects;

public class Characteristics
{
    public int HealthPoints { get; init; }
    public int Movement { get; init; }
    public int Combat { get; init; } 
    public int Dodge { get; init; } 
    public int? Will { get; init; }

    public Characteristics(int healthPoints, int movement, int combat, int dodge, int? will = null)
    {
        if (healthPoints <= 0)
            throw new ArgumentException("Health point must be greater than zero");
        if (movement <= 0)
            throw new ArgumentException("Movement must be greater than zero");
        if (combat <= 0)
            throw new ArgumentException("Combat must be greater than zero");
        if (dodge <= 0)
            throw new ArgumentException("Dodge must be greater than zero");
        if (will <= 0)
            throw new ArgumentException("Will must be greater than zero");
        
        HealthPoints = healthPoints;
        Movement = movement;
        Combat = combat;
        Dodge = dodge;
        Will = will;
    }
}