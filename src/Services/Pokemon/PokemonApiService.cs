using System.Threading.Tasks;

using Pokedex.Infrastructure.Api;

namespace Pokedex.Services;

static class PokemonApiService {
  static public async Task<PokemonApi> GetPokemonAsync(string name) {
    string fullUrl = PokeApiRoutes.Pokemon(name);
    return await PokeApiSerializer.GetAsync<PokemonApi>(fullUrl);
  }
}
