using System.Threading.Tasks;

using Pokedex.Infrastructure.Api;

namespace Pokedex.Services;

static class LocationAreaApiService {
  static public async Task<LocationAreaApi> GetPageAsync(string? url) {
    string fullUrl = url is null ? PokeApiRoutes.LocationAreas() : url;
    return await PokeApiSerializer.GetAsync<LocationAreaApi>(fullUrl);
  }

  static public async Task<LocationAreaDetailApi> GetByNameAsync(string locationArea) {
    string fullUrl = PokeApiRoutes.LocationArea(locationArea);
    return await PokeApiSerializer.GetAsync<LocationAreaDetailApi>(fullUrl);
  }
}
