using System;
using System.Threading.Tasks;

using Pokedex.Domain;
using Pokedex.Infrastructure.Repositories;

namespace Pokedex.Services;

static class CatchService {
  private static bool RollCatch(Pokemon pokemon) {
    int difficulty = pokemon.BaseExperience > 200 ? 200 : pokemon.BaseExperience;
    int roll = Random.Shared.Next(0, 200);
    return roll >= difficulty;
  }

  public static async Task<(bool IsCaught, Pokemon Pokemon)> CatchPokemonAsync(string name) {
    Pokemon pokemon = await PokemonService.GetPokemonAsync(name);
    if (!RollCatch(pokemon)) {
      return (false, pokemon);
    }

    pokemon = await PokemonRepository.InsertPokemonAsync(pokemon);

    return (true, pokemon);
  }
}
