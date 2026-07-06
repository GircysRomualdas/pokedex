using System.Linq;
using Pokedex.Domain;

namespace Pokedex.Infrastructure.Api;

static class LocationAreaDetailMapper {
  public static LocationAreaDetail ToDomain(LocationAreaDetailApi areaDetailApi) {
    return new LocationAreaDetail {
      Name = areaDetailApi.Name,
      PokemonEncounters = areaDetailApi.PokemonEncounters.Select(p => p.Pokemon.Name).ToList()
    };
  }
}