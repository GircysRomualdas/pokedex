using System;
using System.Threading.Tasks;

using Pokedex.State;
using Pokedex.Services;
using Pokedex.Models.Domain;

namespace Pokedex.Commands;

static class CatchCommand {
  public static async Task Run(string[] args, GameState gameState) {
    if (args.Length < 2) {
      Console.WriteLine("Wrong number of arguments!");
      Console.WriteLine("usage: catch <pokemon>");
      return;
    }
    string name = args[1];

    Pokemon pokemon;
    try {
      pokemon = await PokemonService.GetPokemonAsync(name);
    }
    catch (Exception ex) {
      Console.WriteLine(ex.Message);
      return;
    }

    Console.WriteLine($"Throwing a Pokeball at {pokemon.Name}...");
    Console.WriteLine($"{pokemon.Name} was caught!");
  }
}