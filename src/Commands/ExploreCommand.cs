using System;
using System.Threading.Tasks;

using Pokedex.Services;
using Pokedex.Domain;

namespace Pokedex.Commands;

static class ExploreCommand {
  public static async Task Run(string[] args) {
    if (args.Length < 2) {
      Console.WriteLine("Wrong number of arguments!");
      Console.WriteLine("usage: explore <location-area>");
      return;
    }
    string locationArea = args[1];

    LocationAreaDetail locationAreaDetail;
    try {
      locationAreaDetail = await LocationAreaService.GetLocationAreaDetailAsync(locationArea);
    }
    catch (Exception ex) {
      Console.WriteLine(ex.Message);
      return;
    }

    Console.WriteLine("Wild Pokemon found:");
    foreach (var pokemon in locationAreaDetail.PokemonEncounters) {
      Console.WriteLine($" - {pokemon}");
    }
  }
}
