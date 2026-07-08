using System.Threading.Tasks;

using Pokedex.Infrastructure.Api;

namespace Pokedex.Services;

class PokemonApiService {
  private readonly ApiClient apiClient;

  public PokemonApiService(ApiClient apiClient) {
    this.apiClient = apiClient;
  }
  public async Task<PokemonApi> GetPokemonAsync(string name) {
    string fullUrl = PokeApiRoutes.Pokemon(name);
    string responseBody = await apiClient.FetchAsync(fullUrl);
    return Serializer.Deserialize<PokemonApi>(responseBody);
  }
}
