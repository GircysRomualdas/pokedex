using System;

namespace Pokedex.Commands;
static class HelpCommand {
  public static void Execute() {
    Console.WriteLine("Commands:");
    Console.WriteLine("\t exit: Exit the Pokedex");
    Console.WriteLine("\t help: Display a help message");
    Console.WriteLine("\t api: temp test api");
  }
}