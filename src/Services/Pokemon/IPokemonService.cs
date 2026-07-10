using System.Threading.Tasks;
using Pokedex.Domain;

namespace Pokedex.Services;

public interface IPokemonService {
  Task<Pokemon> GetPokemonAsync(string name);
}