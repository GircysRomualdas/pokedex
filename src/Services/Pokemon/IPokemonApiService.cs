using System.Threading.Tasks;
using Pokedex.Infrastructure.Api;

namespace Pokedex.Services;

public interface IPokemonApiService {
  Task<PokemonApi> GetPokemonAsync(string name);
}