using System;

namespace Pokedex.Commands;

static class HelpCommand {
  public static void Run() {
    Console.WriteLine("Commands:");
    Console.WriteLine(" - exit: Exit the Pokedex REPL.");
    Console.WriteLine(" - help: Display a help message with all available commands.");
    Console.WriteLine(" - map: Display the next 20 location areas.");
    Console.WriteLine(" - mapb Display the previous 20 location areas.");
    Console.WriteLine(" - explore <location-area>: List all Pokemon found in a specific location.");
    Console.WriteLine(" - catch <pokemon>: Attempt to catch a wild Pokemon.");
    Console.WriteLine(" - pokedex: Display all Pokemon you've caught.");
  }
}