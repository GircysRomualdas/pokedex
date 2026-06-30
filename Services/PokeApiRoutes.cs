namespace Pokedex.Services;

static class PokeApiRoutes {
  private const string BaseUrl = "https://pokeapi.co/api/v2";

  public static string Pokemon(string name) {
    return $"{BaseUrl}/pokemon/{name}";
  }

  public static string LocationAreas() {
    return $"{BaseUrl}/location-area";
  }

  public static string LocationArea(string name) {
    return $"{BaseUrl}/location-area/{name}";
  }
}