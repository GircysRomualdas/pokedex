using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pokedex.Models.Api;

public class PokemonApi
{
  [JsonPropertyName("name")]
  public required string Name { get; init; }

  [JsonPropertyName("base_experience")]
  public required int BaseExperience { get; init; }

  [JsonPropertyName("height")]
  public required int Height { get; init; }

  [JsonPropertyName("weight")]
  public required int Weight { get; init; }

  // public class StatInfo
  // {
  //   [JsonPropertyName("base_stat")]
  //   public required int BaseStat { get; init; }

  //   public class StatData
  //   {
  //     [JsonPropertyName("name")]
  //     public required string Name { get; init; }

  //     [JsonPropertyName("url")]
  //     public required string Url { get; init; }
  //   }

  //   [JsonPropertyName("stat")]
  //   public required StatData Stat { get; init; }
  // }

  // [JsonPropertyName("stats")]
  // public List<StatInfo> Stats { get; init; } = new();

  public class TypeInfo
  {
    // [JsonPropertyName("slot")]
    // public required int Slot { get; init; }

    public class TypeData
    {
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