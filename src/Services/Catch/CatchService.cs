using System;
using System.Threading.Tasks;

using Pokedex.Domain;
using Pokedex.Infrastructure.Repositories;

namespace Pokedex.Services;

public class CatchService {
  private readonly IPokemonService pokemonService;
  private readonly IPokemonRepository pokemonRepository;
  private readonly ICatchCalculator catchCalculator;

  public CatchService(IPokemonService pokemonService, IPokemonRepository pokemonRepository, ICatchCalculator catchCalculator) {
    this.pokemonService = pokemonService;
    this.pokemonRepository = pokemonRepository;
    this.catchCalculator = catchCalculator;
  }

  public async Task<(bool IsCaught, Pokemon Pokemon)> CatchPokemonAsync(string name) {
    Pokemon pokemon = await pokemonService.GetPokemonAsync(name);
    if (!catchCalculator.CanCatch(pokemon)) {
      return (false, pokemon);
    }

    await pokemonRepository.InsertPokemonAsync(pokemon);

    return (true, pokemon);
  }
}
