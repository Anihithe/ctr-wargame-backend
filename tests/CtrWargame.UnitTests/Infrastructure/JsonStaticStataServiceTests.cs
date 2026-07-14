using CtrWargame.Application.Features;
using CtrWargame.Infrastructure.Services.JsonStaticDataService;
using Microsoft.Extensions.DependencyInjection;

namespace CtrWargame.UnitTests.Infrastructure;

public class JsonStaticStataServiceTests
{
    
    private ServiceProvider _serviceProvider = null!;
    private IStaticDataService _staticDataService = null!;
    
    [SetUp]
    public void Setup()
    {
        _serviceProvider = new ServiceCollection()
            .AddSingleton<IStaticDataService, JsonStaticDataService>()
            .BuildServiceProvider();
        _staticDataService = _serviceProvider.GetRequiredService<IStaticDataService>();
    }
    
    [TearDown]
    public void TearDown()
    {
        _serviceProvider.Dispose();
    }
    
    [Test]
    public void Should_Load_All_Factions_And_Allegiances_Correctly()
    {
        // Act & Assert
        Assert.NotNull(_staticDataService.Factions);
        Assert.NotNull(_staticDataService.Allegiances);
        
        using (Assert.EnterMultipleScope())
        {
            var factions = _staticDataService.Factions.ToList();
            var allegiances = _staticDataService.Allegiances.ToList();
            Assert.That(factions, Has.Count.EqualTo(8));
            Assert.That(allegiances, Has.Count.EqualTo(13));
            
            // Validation structurelle d'une faction
            var alliance = factions.FirstOrDefault(f => f.Name == "L'Alliance");
            Assert.NotNull(alliance);
            Assert.That(alliance!.Id, Is.EqualTo(1));
            Assert.That(alliance.SpecialRules, Contains.Item("Polyvalent"));
            Assert.That(alliance.Origins, Has.Count.EqualTo(6));
            
            // Validation de la valeur null de Volonté pour le Rang 1 de l'Éternité
            var eternite = factions.FirstOrDefault(f => f.Name == "L'Eternité");
            Assert.NotNull(eternite);
            Assert.True(eternite!.BaseProfiles.ContainsKey(1));
            Assert.Null(eternite.BaseProfiles[1].Will);
            
            // Validation structurelle d'une allégeance
            var phalange = allegiances.FirstOrDefault(a => a.Name == "Phalange");
            Assert.NotNull(phalange);
            Assert.That(phalange!.Type, Is.EqualTo("Corps d'armée"));
        }
    }
}