using System;
using System.Threading.Tasks;

using Pokedex.Services;
using Pokedex.Domain;

namespace Pokedex.Commands;

static class CatchCommand {
  public static async Task Run(string[] args) {
    if (args.Length < 2) {
      Console.WriteLine("Wrong number of arguments!");
      Console.WriteLine("usage: catch <pokemon>");
      return;
    }
    string name = args[1];

    Pokemon pokemon;
    bool isCaught;
    try {
      (isCaught, pokemon) = await CatchService.CatchPokemonAsync(name);
    }
    catch (Exception ex) {
      Console.WriteLine(ex.Message);
      return;
    }
    Console.WriteLine($"Throwing a Pokeball at {pokemon.Name}...");

    if (!isCaught) {
      Console.WriteLine($"{pokemon.Name} escaped!");
      return;
    }

    Console.WriteLine($"{pokemon.Name} was caught!");
    Console.WriteLine($"Registered {pokemon.Name} in your Pokedex!");
  }
}
