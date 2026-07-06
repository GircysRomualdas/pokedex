using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Pokedex.Domain;

public class Pokemon {
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }
  public required string Name { get; init; }
  public List<string> Types { get; init; } = new();
  public required int Height { get; init; }
  public required int Weight { get; init; }
  public required int BaseExperience { get; init; }
}
