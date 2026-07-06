using System.Linq;
using Pokedex.Domain;

namespace Pokedex.Infrastructure.Api;

static class LocationAreaMapper {
  public static LocationArea ToDomain(LocationAreaApi locationAreaApi) {
    return new LocationArea {
      Next = locationAreaApi.Next,
      Previous = locationAreaApi.Previous,
      Areas = locationAreaApi.Results.Select(t => t.Name).ToList()
    };
  }
}
