using System;
using System.Threading.Tasks;

using Pokedex.Services;
using Pokedex.Models;
using System.Text.Json;
using System.Net.Http;

namespace Pokedex.Commands;
class MapCommand {
  public static async Task Run() {
    try {
      string responseBody = await PokeAPIServices.PokeAPI("location-area");
      LocationArea? locationArea = JsonSerializer.Deserialize<LocationArea>(responseBody);
      if (locationArea is null) {
        Console.WriteLine("Failed to parse location area.");
        return;
      }

      foreach (var result in locationArea.Results ?? []) {
        Console.WriteLine(result.Name);
      }
    }
    catch (HttpRequestException e) {
        Console.WriteLine($"Network error: {e.Message}");
    }
    catch (JsonException e) {
        Console.WriteLine($"JSON error: {e.Message}");
    }
    catch (Exception e) {
        Console.WriteLine($"Unexpected error: {e.Message}");
    }
  }
}