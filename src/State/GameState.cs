using System.Collections.Generic;
using Pokedex.Domain;

namespace Pokedex.State;

public class GameState {
  public string? NextLocationUrl { get; set; }
  public string? PreviousLocationUrl { get; set; }
  public List<Pokemon> Pokedex { get; init; } = new();
}
