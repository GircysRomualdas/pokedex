using Pokedex.Configuration;

namespace Pokedex.Infrastructure.Api;

public class PokeApiRoutes {
  private readonly string _baseUrl;
  public PokeApiRoutes(string baseUrl) {
    _baseUrl = baseUrl;
  }

  public string Pokemon(string name) {
    return $"{_baseUrl}/pokemon/{name}";
  }

  public string LocationAreas() {
    return $"{_baseUrl}/location-area";
  }

  public string LocationArea(string name) {
    return $"{_baseUrl}/location-area/{name}";
  }
}
