using System.Threading.Tasks;

using Pokedex.Models.Api;

namespace Pokedex.Services;

static class PokemonApiService {
  private const string Endpoint = "pokemon";
  private const string BaseUrl = "https://pokeapi.co/api/v2";

  static public async Task<PokemonApi> GetPokemonAsync(string pokemonName) {
    string fullUrl = $"{BaseUrl}/{Endpoint}/{pokemonName}";
    return await PokeApiSerializer.GetAsync<PokemonApi>(fullUrl);
  }
}