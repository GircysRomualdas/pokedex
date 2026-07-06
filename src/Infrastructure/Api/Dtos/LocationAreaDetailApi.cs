
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pokedex.Infrastructure.Api;

public class LocationAreaDetailApi {
  [JsonPropertyName("name")]
  public required string Name { get; init; }

  public class Pokemon {
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    // [JsonPropertyName("url")]
    // public required string Url { get; init; }
  }
  public class PokemonEncounter {
    [JsonPropertyName("pokemon")]
    public required Pokemon Pokemon { get; init; }
  }
  [JsonPropertyName("pokemon_encounters")]
  public List<PokemonEncounter> PokemonEncounters { get; init; } = new();
}
