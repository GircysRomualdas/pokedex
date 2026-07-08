using System.Threading.Tasks;
using Pokedex.Domain;
using Pokedex.Infrastructure.Api;

namespace Pokedex.Services;

class LocationAreaService {
  private readonly LocationAreaApiService locationAreaApiService;
  public LocationAreaService(LocationAreaApiService locationAreaApiService) {
    this.locationAreaApiService = locationAreaApiService;
  }
  public async Task<LocationArea> GetLocationAreaAsync(string? url) {
    LocationAreaApi locationAreaApi = await locationAreaApiService.GetPageAsync(url);
    return LocationAreaMapper.ToDomain(locationAreaApi);
  }

  public async Task<LocationAreaDetail> GetLocationAreaDetailAsync(string locationArea) {
    LocationAreaDetailApi locationAreaDetailApi = await locationAreaApiService.GetByNameAsync(locationArea);
    return LocationAreaDetailMapper.ToDomain(locationAreaDetailApi);
  }
}