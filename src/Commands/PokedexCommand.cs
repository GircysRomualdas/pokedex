using System;
using System.Linq;

using Pokedex.State;

namespace Pokedex.Commands;

class PokedexCommand {
  private readonly GameState _gameState;
  public PokedexCommand(GameState gameState) {
    _gameState = gameState;
  }

  public void Run() {
    if (_gameState.Pokedex.Count == 0) {
      Console.WriteLine("Your Pokedex is empty. Catch some Pokemon first!");
      return;
    }

    Console.WriteLine("Your Pokedex:");
    foreach (var pokemon in _gameState.Pokedex.OrderBy(p => p.Name)) {
      Console.WriteLine($" - {pokemon.Name,-12} [{string.Join("/", pokemon.Types)}]");
    }
  }
}
