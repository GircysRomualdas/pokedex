using System;
using System.Threading.Tasks;

using Pokedex.Services;
using Pokedex.State;
using Pokedex.Domain;

namespace Pokedex.Commands;

public class MapCommand {
  private readonly GameState _gameState;
  private readonly LocationAreaService _locationAreaService;
  public MapCommand(GameState gameState, LocationAreaService locationAreaService) {
    _gameState = gameState;
    _locationAreaService = locationAreaService;
  }
  public async Task Run(MapDirection direction) {
    string? url = direction == MapDirection.Next ? _gameState.NextLocationUrl : _gameState.PreviousLocationUrl;
    LocationArea locationArea;
    try {
      locationArea = await _locationAreaService.GetLocationAreaAsync(url);
    }
    catch (Exception ex) {
      Console.WriteLine(ex.Message);
      return;
    }

    _gameState.NextLocationUrl = locationArea.Next;
    _gameState.PreviousLocationUrl = locationArea.Previous;

    foreach (var area in locationArea.Areas) {
      Console.WriteLine($" {area}");
    }
  }
}
