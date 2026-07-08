using System.Threading.Tasks;

using Pokedex.Infrastructure.Api;

namespace Pokedex.Services;

class LocationAreaApiService {
  private readonly IApiClient apiClient;
  public LocationAreaApiService(IApiClient apiClient) {
    this.apiClient = apiClient;
  }
  public async Task<LocationAreaApi> GetPageAsync(string? url) {
    string fullUrl = url is null ? PokeApiRoutes.LocationAreas() : url;
    string responseBody = await apiClient.FetchAsync(fullUrl);
    return Serializer.Deserialize<LocationAreaApi>(responseBody);
  }

  public async Task<LocationAreaDetailApi> GetByNameAsync(string locationArea) {
    string fullUrl = PokeApiRoutes.LocationArea(locationArea);
    string responseBody = await apiClient.FetchAsync(fullUrl);
    return Serializer.Deserialize<LocationAreaDetailApi>(responseBody);
  }
}
