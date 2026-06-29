using System;
using System.Threading.Tasks;

using Pokedex.Services;
using Pokedex.Models.Api;
using Pokedex.State;

namespace Pokedex.Commands;
static class MapCommand {
  public static async Task Run(GameState gameState) {
    LocationAreaApi locationArea;
    try {
      locationArea = await LocationAreaService.GetPageAsync(gameState.NextLocationUrl);
    } catch (Exception ex) {
      Console.WriteLine(ex.Message);
      return;
    }

    gameState.NextLocationUrl = locationArea.Next;
    gameState.PreviousLocationUrl = locationArea.Previous;

    foreach (var result in locationArea.Results) {
      Console.WriteLine($" {result.Name}");
    }
  }
}

