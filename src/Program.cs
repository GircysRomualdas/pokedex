using System;
using System.Threading.Tasks;

using Pokedex.State;
using Pokedex.Commands;
using Pokedex.Infrastructure.Repositories;

namespace Pokedex;

class Program {
  static async Task Main() {
    if (!await PokemonRepository.CheckConnectionAsync()) {
      Console.WriteLine("Could not connect to Pokedex database.");
      return;
    }

    GameState gameState = new() {
      Pokedex = await PlayerService.LoadPokedexAsync()
    };

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
          await CatchCommand.Run(gameState, parts);
          break;
        case "pokedex":
          PokedexCommand.Run(gameState);
          break;
        case "inspect":
          InspectCpmmand.Run(gameState, parts);
          break;
        default:
          Console.WriteLine(" Unknown command");
          break;
      }
    }
  }
}
