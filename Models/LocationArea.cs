using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Pokedex.Models;

public class LocationArea {
  [JsonPropertyName("next")]
  public string? Next {get; init;}

  [JsonPropertyName("previous")]
  public string? Previous {get; init;}

  [JsonPropertyName("results")]
  public List<LocationAreaResult>? Results {get; init;}
}

public class LocationAreaResult {
  [JsonPropertyName("name")]
  public string Name {get; init;} = string.Empty;

  [JsonPropertyName("url")]
  public string Url {get; init;} = string.Empty;
}