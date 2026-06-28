using System;

namespace Pokedex.Commands;
static class HelpCommand {
  public static void Run() {
    Console.WriteLine("Commands:");
    Console.WriteLine("\t exit: Exit the Pokedex.");
    Console.WriteLine("\t help: Display a help message.");
    Console.WriteLine("\t map: Display the next location areas.");
    Console.WriteLine("\t explore <location-area>: Explore location areas.");
  }
}