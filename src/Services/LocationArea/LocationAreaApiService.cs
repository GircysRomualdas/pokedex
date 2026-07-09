using System.Threading.Tasks;

using Pokedex.Infrastructure.Api;

namespace Pokedex.Services;

public class LocationAreaApiService : ILocationAreaApiService {
  private readonly IApiClient apiClient;
  private readonly PokeApiRoutes pokeApiRoutes;
  public LocationAreaApiService(IApiClient apiClient, PokeApiRoutes pokeApiRoutes) {
    this.apiClient = apiClient;
    this.pokeApiRoutes = pokeApiRoutes;
  }
  public async Task<LocationAreaApi> GetPageAsync(string? url) {
    string fullUrl = url is null ? pokeApiRoutes.LocationAreas() : url;
    string responseBody = await apiClient.FetchAsync(fullUrl);
    return Serializer.Deserialize<LocationAreaApi>(responseBody);
  }

  public async Task<LocationAreaDetailApi> GetByNameAsync(string locationArea) {
    string fullUrl = pokeApiRoutes.LocationArea(locationArea);
    string responseBody = await apiClient.FetchAsync(fullUrl);
    return Serializer.Deserialize<LocationAreaDetailApi>(responseBody);
  }
}
