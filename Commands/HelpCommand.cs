using System;

namespace Pokedex.Commands;
static class HelpCommand {
  public static void Run() {
    Console.WriteLine(" Commands:");
    Console.WriteLine("   - exit: Exit the Pokedex.");
    Console.WriteLine("   - help: Display a help message.");
    Console.WriteLine("   - map: Display the next location areas.");
    Console.WriteLine("   - explore <location-area>: Explore location areas.");
  }
}