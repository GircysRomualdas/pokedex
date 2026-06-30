using System;

using Pokedex.State;

namespace Pokedex.Commands;

static class PokedexCommand {
  public static void Run(GameState gameState) {
    if (gameState.Pokedex.Count == 0) {
      Console.WriteLine("Your Pokedex is empty. Catch some Pokemon first!");
      return;
    }

    Console.WriteLine(" Your Pokedex:");
    foreach (var pokemon in gameState.Pokedex) {
      Console.WriteLine($" - {pokemon.Name} ({string.Join("/", pokemon.Types)})");
    }
  }
}