using Pokedex.Domain;
using Pokedex.Infrastructure.Repositories.Models;

namespace Pokedex.Infrastructure.Repositories;

static class PokemonDocumentMapper {
  public static PokemonDocument ToDocument(Pokemon pokemon) {
    return new PokemonDocument {
      Name = pokemon.Name,
      Types = pokemon.Types,
      Height = pokemon.Height,
      Weight = pokemon.Weight,
      BaseExperience = pokemon.BaseExperience
    };
  }

  public static Pokemon ToDomain(PokemonDocument document) {
    return new Pokemon {
      Name = document.Name,
      Types = document.Types,
      Height = document.Height,
      Weight = document.Weight,
      BaseExperience = document.BaseExperience
    };
  }
}