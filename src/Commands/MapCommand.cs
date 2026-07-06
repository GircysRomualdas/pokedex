using System;
using System.Threading.Tasks;

using Pokedex.Services;
using Pokedex.State;
using Pokedex.Domain;

namespace Pokedex.Commands;

static class MapCommand {

  public enum MapDirection {
    Next,
    Previous
  }
  public static async Task Run(GameState gameState, MapDirection direction) {
    string? url = direction == MapDirection.Next ? gameState.NextLocationUrl : gameState.PreviousLocationUrl;
    LocationArea locationArea;
    try {
      locationArea = await LocationAreaService.GetLocationAreaAsync(url);
    }
    catch (Exception ex) {
      Console.WriteLine(ex.Message);
      return;
    }

    gameState.NextLocationUrl = locationArea.Next;
    gameState.PreviousLocationUrl = locationArea.Previous;

    foreach (var area in locationArea.Areas) {
      Console.WriteLine($" {area}");
    }
  }
}
