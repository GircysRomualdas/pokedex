using System;

namespace Pokedex.Commands;
static class ExitCommand {
  public static void Execute() {
    Console.WriteLine("Exiting Pokedex!");
    Environment.Exit(0);
  }
}