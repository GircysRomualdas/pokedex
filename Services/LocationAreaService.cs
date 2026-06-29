using System;
using System.Text.Json;
using System.Threading.Tasks;
using Pokedex.Models.Api;

namespace Pokedex.Services;

static class LocationAreaService {
  private const string Path = "location-area";
  static public async Task<LocationAreaApi> GetLocationArea() {
    string responseBody = await PokeApiService.Fetch(Path);

    LocationAreaApi? locationArea;
    try {
      locationArea = JsonSerializer.Deserialize<LocationAreaApi>(responseBody);
    } catch (JsonException ex) {
      throw new JsonException($"Failed to deserialize PokeAPI response for '{Path}' into {nameof(LocationAreaApi)}", ex);
    }
      
    if (locationArea is null) {
      throw new InvalidOperationException($"PokeAPI returned null for '{Path}' when deserializing {nameof(LocationAreaApi)}");
    }

    return locationArea;
  }
}