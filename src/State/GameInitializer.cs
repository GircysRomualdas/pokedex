using System.Threading.Tasks;
using Pokedex.Services;

namespace Pokedex.State;

class GameInitializer {
  private readonly GameState _gameState;
  private readonly IPokemonService _pokemonService;
  public GameInitializer(GameState gameState, IPokemonService pokemonService) {
    _gameState = gameState;
    _pokemonService = pokemonService;
  }

  public async Task InitializeAsync() {
    _gameState.Pokedex = await _pokemonService.GetPokemonsAsync();
  }
}