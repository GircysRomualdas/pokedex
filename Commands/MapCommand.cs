using System;
using System.Threading.Tasks;

using Pokedex.Services;
using Pokedex.Models.Api;
using Pokedex.State;

namespace Pokedex.Commands;
static class MapCommand {

  public enum MapDirection
  {
    Next,
    Previous
  }
  public static async Task Run(GameState gameState, MapDirection direction) {
    string? url = direction == MapDirection.Next ? gameState.NextLocationUrl : gameState.PreviousLocationUrl;
    LocationAreaApi locationArea;
    try {
      locationArea = await LocationAreaApiService.GetPageAsync(url);
    } catch (Exception ex) {
      Console.WriteLine(ex.Message);
      return;
    }

    gameState.NextLocationUrl = locationArea.Next;
    gameState.PreviousLocationUrl = locationArea.Previous;
    Console.WriteLine($"gameState.NextLocationUrl {gameState.NextLocationUrl}");
    Console.WriteLine($"gameState.PreviousLocationUrl {gameState.PreviousLocationUrl}");

    foreach (var result in locationArea.Results) {
      Console.WriteLine($" {result.Name}");
    }
  }
}

