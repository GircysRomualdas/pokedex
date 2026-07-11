using System;
using System.Threading.Tasks;

using Pokedex.Services;
using Pokedex.State;
using Pokedex.Domain;

namespace Pokedex.Commands;

public class MapCommand {
  private readonly GameState gameState;
  private readonly LocationAreaService locationAreaService;
  public MapCommand(GameState gameState, LocationAreaService locationAreaService) {
    this.gameState = gameState;
    this.locationAreaService = locationAreaService;
  }
  public async Task Run(MapDirection direction) {
    string? url = direction == MapDirection.Next ? gameState.NextLocationUrl : gameState.PreviousLocationUrl;
    LocationArea locationArea;
    try {
      locationArea = await locationAreaService.GetLocationAreaAsync(url);
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
