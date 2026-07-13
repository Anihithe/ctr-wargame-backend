using CtrWargame.Domain.ValueObjects;

namespace CtrWargame.UnitTests.Domain.Entities;

public class CharacteristicsTests
{
    [Test]
    public void Characteristics_ShouldThrowException_WhenMovementIsZeroOrNegative()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => new Characteristics(0, 3, 2, 4, 6));
        Assert.Throws<ArgumentException>(() => new Characteristics(-1, 3, 2, 4, 6));
    }

    [Test]
    public void Characteristics_ShouldAllowNullWillpower_ForDecerebrated()
    {
        // Arrange & Act
        var characteristics = new Characteristics(6, 3, 2, 6);
        // Assert
        Assert.IsNull(characteristics.Will);
    }
}