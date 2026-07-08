using System.Collections.Generic;
using System.Threading.Tasks;
using Pokedex.Domain;
using Pokedex.Services;

static class PlayerService {
  public static Task<List<Pokemon>> LoadPokedexAsync() {
    return PokemonService.GetPokemonsAsync();
  }
}