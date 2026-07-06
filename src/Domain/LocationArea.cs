using System.Collections.Generic;

namespace Pokedex.Domain;

public class LocationArea {
  public string? Next { get; init; }
  public string? Previous { get; init; }
  public List<string> Areas { get; init; } = new();
}
