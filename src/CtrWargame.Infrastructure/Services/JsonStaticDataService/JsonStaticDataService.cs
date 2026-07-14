using System.Reflection;
using System.Text.Json;
using CtrWargame.Application.Features;
using CtrWargame.Domain.Entities;

namespace CtrWargame.Infrastructure.Services.JsonStaticDataService;

public class JsonStaticDataService : IStaticDataService
{
    
    private readonly Lazy<IEnumerable<Faction>> _factions;
    private readonly Lazy<IEnumerable<Allegiance>> _allegiances;
    
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    public JsonStaticDataService()
    {
        _factions = new Lazy<IEnumerable<Faction>>(() => LoadEmbeddedResource<Faction>("factions.json"));
        _allegiances = new Lazy<IEnumerable<Allegiance>>(() => LoadEmbeddedResource<Allegiance>("allegiances.json"));
    }
    
    public IEnumerable<Faction> Factions => _factions.Value;
    public IEnumerable<Allegiance> Allegiances => _allegiances.Value;
    private static IEnumerable<T> LoadEmbeddedResource<T>(string fileName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = $"CtrWargame.Infrastructure.Persistence.StaticData.{fileName}";
        using var stream = assembly.GetManifestResourceStream(resourceName)
                           ?? throw new FileNotFoundException($"Embedded resource '{resourceName}' not found in assembly '{assembly.FullName}'.");
        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();
        return JsonSerializer.Deserialize<IEnumerable<T>>(json, JsonOptions)
               ?? throw new InvalidOperationException($"Failed to deserialize embedded resource '{fileName}'.");
    }
    
    // public IEnumerable<Faction> Factions
    // {
    //     get
    //     {
    //         var a = Assembly.GetManifestResourceStream("factions.json");
    //         return field ??= JsonSerializer.Deserialize<IEnumerable<Faction>>(File.ReadAllText("Persistence/StaticData/factions.json"));
    //     }
    // }
    //
    // public IEnumerable<Allegiance>? Allegiances
    // {
    //     get
    //     {
    //         return field ??=
    //             JsonSerializer.Deserialize<IEnumerable<Allegiance>>(File.ReadAllText("Persistence/StaticData/allegiances.json"));
    //     }
    // }
}