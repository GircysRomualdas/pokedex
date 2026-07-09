using System.Threading.Tasks;

using Pokedex.Infrastructure.Api;

namespace Pokedex.Services;

class PokemonApiService {
  private readonly IApiClient apiClient;
  private readonly PokeApiRoutes pokeApiRoutes;

  public PokemonApiService(IApiClient apiClient, PokeApiRoutes pokeApiRoutes) {
    this.apiClient = apiClient;
    this.pokeApiRoutes = pokeApiRoutes;
  }
  public async Task<PokemonApi> GetPokemonAsync(string name) {
    string fullUrl = pokeApiRoutes.Pokemon(name);
    string responseBody = await apiClient.FetchAsync(fullUrl);
    return Serializer.Deserialize<PokemonApi>(responseBody);
  }
}
