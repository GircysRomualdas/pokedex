using Pokedex.Domain;

namespace Pokedex.Services;

public interface ICatchCalculator {
  bool CanCatch(Pokemon pokemon);
}