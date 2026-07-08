using System;
using System.Linq;

using Pokedex.State;

namespace Pokedex.Commands;

class PokedexCommand {
  private readonly GameState gameState;
  public PokedexCommand(GameState gameState) {
    this.gameState = gameState;
  }

  public void Run() {
    if (gameState.Pokedex.Count == 0) {
      Console.WriteLine("Your Pokedex is empty. Catch some Pokemon first!");
      return;
    }

    Console.WriteLine("Your Pokedex:");
    foreach (var pokemon in gameState.Pokedex.OrderBy(p => p.Name)) {
      Console.WriteLine($" - {pokemon.Name,-12} [{string.Join("/", pokemon.Types)}]");
    }
  }
}
