using System.Threading.Tasks;

using Pokedex.Infrastructure.Api;

namespace Pokedex.Services;

public class PokemonApiService : IPokemonApiService {
  private readonly IApiClient _apiClient;
  private readonly PokeApiRoutes _pokeApiRoutes;

  public PokemonApiService(IApiClient apiClient, PokeApiRoutes pokeApiRoutes) {
    _apiClient = apiClient;
    _pokeApiRoutes = pokeApiRoutes;
  }
  public async Task<PokemonApi> GetPokemonAsync(string name) {
    string fullUrl = _pokeApiRoutes.Pokemon(name);
    string responseBody = await _apiClient.FetchAsync(fullUrl);
    return Serializer.Deserialize<PokemonApi>(responseBody);
  }
}
