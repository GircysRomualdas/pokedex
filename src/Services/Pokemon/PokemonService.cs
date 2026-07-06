using System.Threading.Tasks;

using Pokedex.Infrastructure.Api;
using Pokedex.Domain;

namespace Pokedex.Services;

static class PokemonService {
  static public async Task<Pokemon> GetPokemonAsync(string name) {
    PokemonApi pokemonApi = await PokemonApiService.GetPokemonAsync(name);
    return PokemonMapper.ToDomain(pokemonApi);
  }
}
