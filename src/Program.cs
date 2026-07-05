using System;
using System.Threading.Tasks;

using Pokedex.State;
using Pokedex.Commands;

namespace Pokedex;

class Program {
  static async Task Main() {
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
          await CatchCommand.Run(parts);
          break;
        case "pokedex":
          await PokedexCommand.Run();
          break;
        default:
          Console.WriteLine(" Unknown command");
          break;
      }
    }
  }
}