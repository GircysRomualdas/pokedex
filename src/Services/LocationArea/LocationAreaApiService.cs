using System.Threading.Tasks;

using Pokedex.Infrastructure.Api;

namespace Pokedex.Services;

public class LocationAreaApiService : ILocationAreaApiService {
  private readonly IApiClient _apiClient;
  private readonly PokeApiRoutes _pokeApiRoutes;
  public LocationAreaApiService(IApiClient apiClient, PokeApiRoutes pokeApiRoutes) {
    _apiClient = apiClient;
    _pokeApiRoutes = pokeApiRoutes;
  }
  public async Task<LocationAreaApi> GetPageAsync(string? url) {
    string fullUrl = url is null ? _pokeApiRoutes.LocationAreas() : url;
    string responseBody = await _apiClient.FetchAsync(fullUrl);
    return Serializer.Deserialize<LocationAreaApi>(responseBody);
  }

  public async Task<LocationAreaDetailApi> GetByNameAsync(string locationArea) {
    string fullUrl = _pokeApiRoutes.LocationArea(locationArea);
    string responseBody = await _apiClient.FetchAsync(fullUrl);
    return Serializer.Deserialize<LocationAreaDetailApi>(responseBody);
  }
}
