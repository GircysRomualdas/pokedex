using System;
using Pokedex.Services;
using System.Threading.Tasks;

namespace Pokedex.Commands;
class MapCommand {
  public static async Task Run() {
    string responseBody = await PokeAPIServices.PokeAPI("location-area");
    Console.WriteLine(responseBody);
  }
}