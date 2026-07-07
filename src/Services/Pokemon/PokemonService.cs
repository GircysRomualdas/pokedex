using System.Threading.Tasks;
using System.Collections.Generic;

using Pokedex.Infrastructure.Api;
using Pokedex.Domain;
using Pokedex.Infrastructure.Repositories;

namespace Pokedex.Services;

static class PokemonService {
  static public async Task<Pokemon> GetPokemonAsync(string name) {
    PokemonApi pokemonApi = await PokemonApiService.GetPokemonAsync(name);
    return PokemonMapper.ToDomain(pokemonApi);
  }

  static public Task<List<Pokemon>> GetPokemonsAsync() {
    return PokemonRepository.GetPokemonsAsync();
  }
}
