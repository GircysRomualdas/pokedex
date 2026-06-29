using System;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;

using Pokedex.Services;
using Pokedex.Models.API;

namespace Pokedex.Commands;
static class MapCommand {
  public static async Task Run() {
    string responseBody;
    try {
      responseBody = await PokeAPIServices.FetchAsync("location-area");
    }
    catch (HttpRequestException e) {
        Console.WriteLine($"Network error: {e.Message}");
        return;
    } catch (Exception e) {
        Console.WriteLine($"Unexpected error: {e.Message}");
        return;
    }
    
    LocationAreaAPI? locationAreaAPI;
    try {
      locationAreaAPI = JsonSerializer.Deserialize<LocationAreaAPI>(responseBody);
    } catch (JsonException e) {
        Console.WriteLine($"JSON error: {e.Message}");
        return;
    } catch (Exception e) {
        Console.WriteLine($"Unexpected error: {e.Message}");
        return;
    }
      
    if (locationAreaAPI is null) {
      Console.WriteLine("Failed to parse location area.");
      return;
    }

    foreach (var result in locationAreaAPI.Results) {
      Console.WriteLine(result.Name);
    }
  }
}