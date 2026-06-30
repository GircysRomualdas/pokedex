using System;

namespace Pokedex.Commands;

static class ExitCommand {
  public static void Run() {
    Console.WriteLine("Exiting Pokedex!");
    Environment.Exit(0);
  }
}