using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pokedex.Services;

static class PokeApiSerializer {
  public static async Task<T> GetAsync<T>(string url) {
    string responseBody = await PokeApiClient.FetchAsync(url);

    T? data;
    try {
      data = JsonSerializer.Deserialize<T>(responseBody);
    }
    catch (JsonException ex) {
      throw new JsonException($"Failed to deserialize PokeAPI response for '{url}' into {typeof(T).Name}", ex);
    }

    if (data is null) {
      throw new Exception($"PokeAPI returned null for '{url}' when deserializing {typeof(T).Name}");
    }

    return data;
  }
}