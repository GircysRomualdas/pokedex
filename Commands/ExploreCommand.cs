using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

using Pokedex.Services;
using Pokedex.Models.Api;

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
      locationAreaDetail = await LocationAreaService.GetByNameAsync(locationArea);
    } catch (Exception ex) {
      Console.WriteLine($"Error: {ex.Message}");
      return;
    }

    Console.WriteLine(" Found Pokemon:");
    foreach (var encounter in locationAreaDetail.PokemonEncounters) {
      Console.WriteLine($" - {encounter.Pokemon.Name}");
    }
  }
}