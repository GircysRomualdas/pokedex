
using System.Threading.Tasks;
using Pokedex.Domain;
using Pokedex.Infrastructure.Api;

namespace Pokedex.Services;

static class LocationAreaService {
  static public async Task<LocationArea> GetLocationAreaAsync(string? url) {
    LocationAreaApi locationAreaApi = await LocationAreaApiService.GetPageAsync(url);
    return LocationAreaMapper.ToDomain(locationAreaApi);
  }
}