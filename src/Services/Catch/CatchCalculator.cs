using System;
using Pokedex.Domain;

namespace Pokedex.Services;

public class CatchCalculator : ICatchCalculator {
  public bool CanCatch(Pokemon pokemon) {
    int difficulty = Math.Min(pokemon.BaseExperience, 199);
    int roll = Random.Shared.Next(0, 200);
    return roll >= difficulty;
  }
}