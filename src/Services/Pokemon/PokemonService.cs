using System.Threading.Tasks;
using System.Collections.Generic;

using Pokedex.Infrastructure.Api;
using Pokedex.Domain;
using Pokedex.Infrastructure.Repositories;

namespace Pokedex.Services;

class PokemonService {
  private readonly PokemonApiService pokemonApiService;
  private readonly PokemonRepository pokemonRepository;

  public PokemonService(PokemonApiService pokemonApiService, PokemonRepository pokemonRepository) {
    this.pokemonApiService = pokemonApiService;
    this.pokemonRepository = pokemonRepository;
  }

  public async Task<Pokemon> GetPokemonAsync(string name) {
    PokemonApi pokemonApi = await pokemonApiService.GetPokemonAsync(name);
    return PokemonMapper.ToDomain(pokemonApi);
  }

  public Task<List<Pokemon>> GetPokemonsAsync() {
    return pokemonRepository.GetPokemonsAsync();
  }
}