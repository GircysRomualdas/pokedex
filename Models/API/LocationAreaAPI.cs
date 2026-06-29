using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Pokedex.Models.API;

public class LocationAreaAPI {
  [JsonPropertyName("next")]
  public string? Next {get; init;}

  [JsonPropertyName("previous")]
  public string? Previous {get; init;}

  public class LocationAreaResult {
    [JsonPropertyName("name")]
    public string Name {get; init;} = string.Empty;

    [JsonPropertyName("url")]
    public string Url {get; init;} = string.Empty;
  }
  [JsonPropertyName("results")]
  public List<LocationAreaResult> Results {get; init;} = new();
}
