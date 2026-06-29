using System;
using System.Threading.Tasks;

using Pokedex.Commands;

namespace Pokedex;
class Program {
  static async Task Main() {
    Console.WriteLine(" Welcome to the Pokedex!");
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
          await MapCommand.Run();
          break;
        case "explore":
          await ExploreCommand.Run(parts);
          break;
        default:
          Console.WriteLine(" Unknown command");
          break;
      }
    }
  }
}