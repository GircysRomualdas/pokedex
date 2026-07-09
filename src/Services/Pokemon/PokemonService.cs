using System.Threading.Tasks;
using System.Collections.Generic;

using Pokedex.Infrastructure.Api;
using Pokedex.Domain;
using Pokedex.Infrastructure.Repositories;

namespace Pokedex.Services;

public class PokemonService {
  private readonly IPokemonApiService pokemonApiService;
  private readonly IPokemonRepository pokemonRepository;

  public PokemonService(IPokemonApiService pokemonApiService, IPokemonRepository pokemonRepository) {
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