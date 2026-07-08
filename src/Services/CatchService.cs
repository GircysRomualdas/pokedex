using System;
using System.Threading.Tasks;

using Pokedex.Domain;
using Pokedex.Infrastructure.Repositories;

namespace Pokedex.Services;

class CatchService {
  private readonly PokemonService pokemonService;
  private readonly PokemonRepository pokemonRepository;

  public CatchService(PokemonService pokemonService, PokemonRepository pokemonRepository) {
    this.pokemonService = pokemonService;
    this.pokemonRepository = pokemonRepository;
  }
  private bool RollCatch(Pokemon pokemon) {
    int difficulty = Math.Min(pokemon.BaseExperience, 199);
    int roll = Random.Shared.Next(0, 200);
    return roll >= difficulty;
  }

  public async Task<(bool IsCaught, Pokemon Pokemon)> CatchPokemonAsync(string name) {
    Pokemon pokemon = await pokemonService.GetPokemonAsync(name);
    if (!RollCatch(pokemon)) {
      return (false, pokemon);
    }

    pokemon = await pokemonRepository.InsertPokemonAsync(pokemon);

    return (true, pokemon);
  }
}
