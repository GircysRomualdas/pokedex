using System;
using System.Threading.Tasks;

using Pokedex.Services;
using Pokedex.Models.Api;

namespace Pokedex.Commands;
static class MapCommand {
  public static async Task Run() {
    LocationAreaApi locationArea;
    try {
      locationArea = await LocationAreaService.GetAll();
    } catch (Exception ex) {
      Console.WriteLine($"Error: {ex.Message}");
      return;
    }

    foreach (var result in locationArea.Results) {
      Console.WriteLine($" {result.Name}");
    }
  }
}