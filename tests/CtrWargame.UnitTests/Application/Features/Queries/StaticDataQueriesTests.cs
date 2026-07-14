using CtrWargame.Application.Features;
using CtrWargame.Application.Features.Queries;
using CtrWargame.Domain.Entities;
using CtrWargame.Domain.ValueObjects;

namespace CtrWargame.UnitTests.Application.Features.Queries;
public class StaticDataQueriesTests
{
    private class FakeStaticDataService : IStaticDataService
    {
        public IEnumerable<Faction> Factions { get; set; } = [];
        public IEnumerable<Allegiance> Allegiances { get; set; } = [];
    }
    private FakeStaticDataService _staticDataService = null!;
    [SetUp]
    public void Setup()
    {
        _staticDataService = new FakeStaticDataService();
    }
    [Test]
    public async Task GetFactionsQueryHandler_ShouldReturnAllFactionsMappedToResponse()
    {
        // Arrange
        var baseProfiles = new Dictionary<int, Characteristics>
        {
            { 1, new Characteristics(10, 5, 4, 3, 2) }
        };
        var origins = new List<Origin> { new("Origin1", "Description1") };
        var specialRules = new List<string> { "Rule1" };
        _staticDataService.Factions = new List<Faction>
        {
            new(1, "FactionTest", baseProfiles, specialRules, origins)
        };
        var handler = new GetFactionsQueryHandler(_staticDataService);
        var query = new GetFactionsQuery();
        // Act
        var result = (await handler.HandleAsync(query, CancellationToken.None)).ToList();
        // Assert
        Assert.That(result, Has.Count.EqualTo(1));
        var factionDto = result[0];
        Assert.That(factionDto.Name, Is.EqualTo("FactionTest"));
        Assert.That(factionDto.BaseProfiles, Is.EqualTo(baseProfiles));
        Assert.That(factionDto.SpecialRules, Is.EqualTo(specialRules));
        Assert.That(factionDto.Origins, Is.EqualTo(origins));
    }
    [Test]
    public async Task GetAllegianceQueryHandler_ShouldReturnAllAllegiancesMappedToResponse()
    {
        // Arrange
        _staticDataService.Allegiances = new List<Allegiance>
        {
            new(1, "Type1", "AllegianceTest", "DescriptionTest")
        };
        var handler = new GetAllegianceQueryHandler(_staticDataService);
        var query = new GetAllegiancesQuery();
        // Act
        var result = (await handler.HandleAsync(query, CancellationToken.None)).ToList();
        // Assert
        Assert.That(result, Has.Count.EqualTo(1));
        var allegianceDto = result[0];
        Assert.That(allegianceDto.Type, Is.EqualTo("Type1"));
        Assert.That(allegianceDto.Name, Is.EqualTo("AllegianceTest"));
        Assert.That(allegianceDto.Description, Is.EqualTo("DescriptionTest"));
    }
}