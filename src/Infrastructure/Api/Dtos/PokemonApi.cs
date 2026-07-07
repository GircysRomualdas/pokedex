using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pokedex.Infrastructure.Api;

public class PokemonApi {
  [JsonPropertyName("name")]
  public required string Name { get; init; }

  [JsonPropertyName("base_experience")]
  public required int BaseExperience { get; init; }

  [JsonPropertyName("height")]
  public required int Height { get; init; }

  [JsonPropertyName("weight")]
  public required int Weight { get; init; }

  public class TypeInfo {
    public class TypeData {
      [JsonPropertyName("name")]
      public required string Name { get; init; }

      [JsonPropertyName("url")]
      public required string Url { get; init; }
    }

    [JsonPropertyName("type")]
    public required TypeData Type { get; init; }
  }

  [JsonPropertyName("types")]
  public List<TypeInfo> Types { get; init; } = new();
}
