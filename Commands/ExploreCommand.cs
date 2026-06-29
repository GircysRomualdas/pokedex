using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

using Pokedex.Services;
using Pokedex.Models.API;

namespace Pokedex.Commands;

static class ExploreCommand {
  public static async Task Run(string[] args) {
    if (args.Length < 2) {
      Console.WriteLine("Wrong number of arguments!");
      Console.WriteLine("usage: explore <location-area>");
      return;
    }

    string locationArea = args[1];
    string path = $"location-area/{locationArea}";
    string responseBody;
    try {
      responseBody = await PokeAPIServices.FetchAsync(path);
    }
    catch (HttpRequestException e) {
        Console.WriteLine($"Network error: {e.Message}");
        return;
    } catch (Exception e) {
        Console.WriteLine($"Unexpected error: {e.Message}");
        return;
    }
    
    LocationAreaDetailAPI? locationAreaDetailAPI;
    try {
      locationAreaDetailAPI = JsonSerializer.Deserialize<LocationAreaDetailAPI>(responseBody);
    } catch (JsonException e) {
        Console.WriteLine($"JSON error: {e.Message}");
        return;
    } catch (Exception e) {
        Console.WriteLine($"Unexpected error: {e.Message}");
        return;
    }

    if (locationAreaDetailAPI is null) {
      Console.WriteLine("Failed to parse location area.");
      return;
    }

    Console.WriteLine("Found Pokemon:");

    foreach (var encounter in locationAreaDetailAPI.PokemonEncounters) {
      Console.WriteLine($" - {encounter.Pokemon.Name}");
    }
  }
}