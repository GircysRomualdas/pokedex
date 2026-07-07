using System.Linq;
using Pokedex.Domain;

namespace Pokedex.Infrastructure.Api;

static class PokemonMapper {
  public static Pokemon ToDomain(PokemonApi api) {
    return new Pokemon {
      Name = api.Name,
      Types = api.Types.Select(t => t.Type.Name).ToList(),
      Height = api.Height,
      Weight = api.Weight,
      BaseExperience = api.BaseExperience
    };
  }
}
