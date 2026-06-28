using System;
using System.Threading.Tasks;

using Pokedex.Services;
using Pokedex.Commands;

namespace Pokedex;
class Program {
  static async Task Main() {
    Console.WriteLine("Welcome to the Pokedex!");
    HelpCommand.Execute();

    while (true) {
      Console.Write("> ");
      string? input = Console.ReadLine();
      if (string.IsNullOrWhiteSpace(input)) continue;

      string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

      switch (parts[0]) {
        case "exit":
          ExitCommand.Execute();
          break;
        case "help":
          HelpCommand.Execute();
          break;
        case "api":
          await Api.ApiPoke();
          break;
        default:
          Console.WriteLine("Unknown command");
          break;
      }
    }
  }
}