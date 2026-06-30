using Pokedex.Models.Api;
using Pokedex.Models.Domain;

namespace Pokedex.Services;

static class PokemonMapper {
  public static Pokemon ToDomain(PokemonApi api) {
    return new Pokemon {
      Name = api.Name,
      Type = api.Types[0].Type.Name
    };
  }
}