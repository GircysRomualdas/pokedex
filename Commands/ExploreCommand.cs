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
    string path = $"location-area/{locationArea}";
    string responseBody;
    try {
      responseBody = await PokeApiService.Fetch(path);
    }
    catch (HttpRequestException e) {
        Console.WriteLine($"Network error: {e.Message}");
        return;
    } catch (Exception e) {
        Console.WriteLine($"Unexpected error: {e.Message}");
        return;
    }
    
    LocationAreaDetailApi? locationAreaDetailApi;
    try {
      locationAreaDetailApi = JsonSerializer.Deserialize<LocationAreaDetailApi>(responseBody);
    } catch (JsonException e) {
        Console.WriteLine($"JSON error: {e.Message}");
        return;
    } catch (Exception e) {
        Console.WriteLine($"Unexpected error: {e.Message}");
        return;
    }

    if (locationAreaDetailApi is null) {
      Console.WriteLine("Failed to parse location area.");
      return;
    }

    Console.WriteLine(" Found Pokemon:");

    foreach (var encounter in locationAreaDetailApi.PokemonEncounters) {
      Console.WriteLine($" - {encounter.Pokemon.Name}");
    }
  }
}