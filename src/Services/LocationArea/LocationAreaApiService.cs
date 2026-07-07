using System.Threading.Tasks;
using System.Text.Json;

using Pokedex.Infrastructure.Api;

namespace Pokedex.Services;

static class LocationAreaApiService {
  static public async Task<LocationAreaApi> GetPageAsync(string? url) {
    string fullUrl = url is null ? PokeApiRoutes.LocationAreas() : url;
    string responseBody = await ApiClient.FetchAsync(fullUrl);
    return Serializer.Deserialize<LocationAreaApi>(responseBody);
  }

  static public async Task<LocationAreaDetailApi> GetByNameAsync(string locationArea) {
    string fullUrl = PokeApiRoutes.LocationArea(locationArea);
    string responseBody = await ApiClient.FetchAsync(fullUrl);
    return Serializer.Deserialize<LocationAreaDetailApi>(responseBody);
  }
}
