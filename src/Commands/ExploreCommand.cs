using System;
using System.Threading.Tasks;

using Pokedex.Services;
using Pokedex.Infrastructure.Api;

namespace Pokedex.Commands;

static class ExploreCommand {
  public static async Task Run(string[] args) {
    if (args.Length < 2) {
      Console.WriteLine("Wrong number of arguments!");
      Console.WriteLine("usage: explore <location-area>");
      return;
    }
    string locationArea = args[1];

    LocationAreaDetailApi locationAreaDetail;
    try {
      locationAreaDetail = await LocationAreaApiService.GetByNameAsync(locationArea);
    }
    catch (Exception ex) {
      Console.WriteLine(ex.Message);
      return;
    }

    Console.WriteLine("Wild Pokémon found:");
    foreach (var encounter in locationAreaDetail.PokemonEncounters) {
      Console.WriteLine($" - {encounter.Pokemon.Name}");
    }
  }
}
