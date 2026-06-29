using System.Collections.Generic;
using Pokedex.Models.Domain;

namespace Pokedex.State;

interface IReadOnlyGameState {
  string? NextLocationUrl { get; }
  string? PreviousLocationUrl { get; }
  IReadOnlyList<Pokemon> Pokedex { get; }
}

class GameState : IReadOnlyGameState {
  public string? NextLocationUrl { get; set; }
  public string? PreviousLocationUrl { get; set; }

  public List<Pokemon> Pokedex { get; init; } = new();
  IReadOnlyList<Pokemon> IReadOnlyGameState.Pokedex => Pokedex;
}