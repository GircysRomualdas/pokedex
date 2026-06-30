using System.Threading.Tasks;

using Pokedex.Models.Api;
using Pokedex.Models.Domain;

namespace Pokedex.Services;

static class PokemonService {
  static public async Task<Pokemon> GetPokemonAsync(string name) {
    PokemonApi pokemonApi = await PokemonApiService.GetPokemonAsync(name);
    return PokemonMapper.ToDomain(pokemonApi);
  }
}