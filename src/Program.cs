using System;
using System.Threading.Tasks;

using Pokedex.State;
using Pokedex.Infrastructure;

using Pokedex.Commands;
using Pokedex.Models.Domain;
using System.Collections.Generic;

namespace Pokedex;

class Program {
  static async Task Main() {
    // temp 
    Console.WriteLine("------------------------");
    await Database.RunTest();
    DatabaseMigrator.Run();
    await Database.GetPokemons();
    var pokemon = new Pokemon {
      Name = "Test name",
      Types = new List<string> { "A", "B" },
      Height = 111,
      Weight = 222,
      BaseExperience = 333
    };
    await Database.InsertPokemon(pokemon);
    await Database.GetPokemons();
    Console.WriteLine("------------------------");
    // temp

    GameState gameState = new();

    Console.WriteLine("Welcome to the Pokedex!");
    HelpCommand.Run();

    while (true) {
      Console.Write("> ");
      string? input = Console.ReadLine();
      if (string.IsNullOrWhiteSpace(input)) continue;

      string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

      switch (parts[0]) {
        case "exit":
          ExitCommand.Run();
          break;
        case "help":
          HelpCommand.Run();
          break;
        case "map":
          await MapCommand.Run(gameState, MapCommand.MapDirection.Next);
          break;
        case "mapb":
          await MapCommand.Run(gameState, MapCommand.MapDirection.Previous);
          break;
        case "explore":
          await ExploreCommand.Run(parts);
          break;
        case "catch":
          await CatchCommand.Run(parts, gameState);
          break;
        case "pokedex":
          PokedexCommand.Run(gameState);
          break;
        default:
          Console.WriteLine(" Unknown command");
          break;
      }
    }
  }
}