using System;
using System.Text.Json;

namespace Pokedex.Infrastructure.Api;

static class Serializer {
  public static T Deserialize<T>(string rawData) {
    T? data;
    try {
      data = JsonSerializer.Deserialize<T>(rawData);
    }
    catch (JsonException ex) {
      throw new JsonException($"Failed to deserialize into {typeof(T).Name}", ex);
    }

    if (data is null) {
      throw new Exception($"Deserialized data is null for {typeof(T).Name}");
    }

    return data;
  }
}
