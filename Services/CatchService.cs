using System;

using Pokedex.Models.Domain;

namespace Pokedex.Services;

static class CatchService {
  public static bool TryCatch(Pokemon pokemon) {
    int difficulty = pokemon.BaseExperience;
    int roll = Random.Shared.Next(0, 200);
    return roll >= difficulty;
  }
}