using System;
using System.Linq;
using Pokedex.Domain;
using Pokedex.State;

namespace Pokedex.Commands;

static class InspectCpmmand {
  public static void Run(GameState gameState, string[] args) {
    if (args.Length < 2) {
      Console.WriteLine("Wrong number of arguments!");
      Console.WriteLine("usage: inspect <pokemon>");
      return;
    }
    string name = args[1];

    Pokemon? pokemon = gameState.Pokedex.FirstOrDefault(p => p.Name == name);

    if (pokemon is null) {
      Console.WriteLine($"Your Pokedex does not contain {name}");
      return;
    }

    Console.WriteLine($"Name: {pokemon.Name}");
    Console.WriteLine($"Height: {pokemon.Height}");
    Console.WriteLine($"Weight: {pokemon.Weight}");
    Console.WriteLine($"Types: [{string.Join("/", pokemon.Types)}]");
    Console.WriteLine($"Base Experience: {pokemon.BaseExperience}");
  }
}