using System.Threading.Tasks;
using System.Collections.Generic;

using Pokedex.Infrastructure.Api;
using Pokedex.Domain;
using Pokedex.Infrastructure.Repositories;

namespace Pokedex.Services;

public class PokemonService : IPokemonService {
  private readonly IPokemonApiService _pokemonApiService;
  private readonly IPokemonRepository _pokemonRepository;

  public PokemonService(IPokemonApiService pokemonApiService, IPokemonRepository pokemonRepository) {
    _pokemonApiService = pokemonApiService;
    _pokemonRepository = pokemonRepository;
  }

  public async Task<Pokemon> GetPokemonAsync(string name) {
    PokemonApi pokemonApi = await _pokemonApiService.GetPokemonAsync(name);
    return PokemonMapper.ToDomain(pokemonApi);
  }

  public Task<List<Pokemon>> GetPokemonsAsync() {
    return _pokemonRepository.GetPokemonsAsync();
  }
}