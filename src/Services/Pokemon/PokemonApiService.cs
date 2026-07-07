using System.Threading.Tasks;

using Pokedex.Infrastructure.Api;

namespace Pokedex.Services;

static class PokemonApiService {
  static public async Task<PokemonApi> GetPokemonAsync(string name) {
    string fullUrl = PokeApiRoutes.Pokemon(name);
    string responseBody = await ApiClient.FetchAsync(fullUrl);
    return Serializer.Deserialize<PokemonApi>(responseBody);
  }
}
