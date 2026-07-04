using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pokedex.Infrastructure;
using Pokedex.Models.Domain;
using Pokedex.State;

namespace Pokedex.Commands;

static class PokedexCommand {
  public static async Task Run(GameState gameState) {
    List<Pokemon> pokemons = await PokemonRepository.GetAllAsync();
    if (pokemons.Count == 0) {
      Console.WriteLine("Your Pokedex is empty. Catch some Pokemon first!");
      return;
    }

    Console.WriteLine("Your Pokedex:");
    foreach (var pokemon in pokemons.OrderBy(p => p.Name)) {
      Console.WriteLine($" - {pokemon.Name,-12} [{string.Join("/", pokemon.Types)}]");
    }
  }
}