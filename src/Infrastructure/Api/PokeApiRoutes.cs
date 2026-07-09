namespace Pokedex.Infrastructure.Api;

public class PokeApiRoutes {
  private readonly string baseUrl;
  public PokeApiRoutes(string baseUrl) {
    this.baseUrl = baseUrl;
  }

  public string Pokemon(string name) {
    return $"{baseUrl}/pokemon/{name}";
  }

  public string LocationAreas() {
    return $"{baseUrl}/location-area";
  }

  public string LocationArea(string name) {
    return $"{baseUrl}/location-area/{name}";
  }
}
