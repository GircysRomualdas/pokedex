
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pokedex.Models.API;
public class LocationAreaDetailAPI {
  [JsonPropertyName("name")]
  public required string Name {get; init;}

  public class Pokemon {
    [JsonPropertyName("name")]
    public required string Name {get; init;}
  }
  public class PokemonEncounter {
    [JsonPropertyName("pokemon")]
    public required Pokemon Pokemon {get; init;}
  }
  [JsonPropertyName("pokemon_encounters")]
  public List<PokemonEncounter> PokemonEncounters {get; init;} = new();
}