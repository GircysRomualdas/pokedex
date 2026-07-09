using System.Collections.Generic;
using System.Threading.Tasks;
using Pokedex.Domain;

namespace Pokedex.Infrastructure.Repositories;

public interface IPokemonRepository {
  Task<Pokemon> InsertPokemonAsync(Pokemon pokemon);
  Task<List<Pokemon>> GetPokemonsAsync();
}