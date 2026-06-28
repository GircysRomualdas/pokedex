using System;
using System.Threading.Tasks;

namespace pokedex;
class Program {
  static async Task Main() {
    Console.WriteLine("Welcome to the Pokedex!");
    Help();

    while (true) {
      Console.Write("> ");
      string? input = Console.ReadLine();
      if (string.IsNullOrWhiteSpace(input)) continue;

      string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

      switch (parts[0]) {
        case "exit":
          Console.WriteLine("Exiting Pokedex!");
          return;
        case "help":
          Console.WriteLine("You can use");
          Help();
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
  static void Help() {
    Console.WriteLine("Commands:");
    Console.WriteLine("\t exit: Exit the Pokedex");
    Console.WriteLine("\t help: Display a help message");
    Console.WriteLine("\t api: temp test api");
  }
}