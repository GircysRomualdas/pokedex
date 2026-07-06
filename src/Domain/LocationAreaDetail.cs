
using System.Collections.Generic;

namespace Pokedex.Domain;

public class LocationAreaDetail {
  public required string Name { get; init; }
  public List<string> PokemonEncounters { get; init; } = new();
}