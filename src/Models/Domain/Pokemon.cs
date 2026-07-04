using System.Collections.Generic;

namespace Pokedex.Models.Domain;

public class Pokemon {

  public int? Id { get; set; }
  public required string Name { get; init; }
  public List<string> Types { get; init; } = new();
  public required int Height { get; init; }
  public required int Weight { get; init; }
  public required int BaseExperience { get; init; }
}