using System;
using System.Threading.Tasks;

using Pokedex.Domain;
using Pokedex.Infrastructure.Repositories;

namespace Pokedex.Services;

public class CatchService {
  private readonly IPokemonService _pokemonService;
  private readonly IPokemonRepository _pokemonRepository;
  private readonly ICatchCalculator _catchCalculator;

  public CatchService(IPokemonService pokemonService, IPokemonRepository pokemonRepository, ICatchCalculator catchCalculator) {
    _pokemonService = pokemonService;
    _pokemonRepository = pokemonRepository;
    _catchCalculator = catchCalculator;
  }

  public async Task<(bool IsCaught, Pokemon Pokemon)> CatchPokemonAsync(string name) {
    Pokemon pokemon = await _pokemonService.GetPokemonAsync(name);
    if (!_catchCalculator.CanCatch(pokemon)) {
      return (false, pokemon);
    }

    await _pokemonRepository.InsertPokemonAsync(pokemon);

    return (true, pokemon);
  }
}
