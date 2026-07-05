using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pokedex.Infrastructure;
using Pokedex.Models.Domain;

namespace Pokedex.Commands;

static class PokedexCommand {
  public static async Task Run() {
    List<Pokemon> pokemons = await PokemonRepository.GetPokemonsAsync();
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