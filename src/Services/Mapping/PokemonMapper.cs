using System.Linq;
using Pokedex.Models.Api;
using Pokedex.Models.Domain;

namespace Pokedex.Services;

static class PokemonMapper {
  public static Pokemon ToDomain(PokemonApi api) {
    return new Pokemon {
      Id = null,
      Name = api.Name,
      Types = api.Types.Select(t => t.Type.Name).ToList(),
      Height = api.Height,
      Weight = api.Weight,
      BaseExperience = api.BaseExperience
    };
  }
}