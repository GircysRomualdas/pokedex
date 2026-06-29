using System;
using System.Threading.Tasks;

using Pokedex.State;
using Pokedex.Models.Api;
using Pokedex.Services;
using Pokedex.Models.Domain;

namespace Pokedex.Commands;

static class CatchCommand
{
  public static async Task Run(string[] args, GameState gameState)
  {
    if (args.Length < 2)
    {
      Console.WriteLine("Wrong number of arguments!");
      Console.WriteLine("usage: catch <pokemon>");
      return;
    }
    string pokemon = args[1];

    PokemonApi pokemonApi;
    try
    {
      pokemonApi = await PokemonApiService.GetPokemonAsync(pokemon);
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
      return;
    }



    Console.WriteLine($"Throwing a Pokeball at {pokemonApi.Name}...");
    gameState.Pokedex.Add(new Pokemon { Name = pokemonApi.Name, Type = pokemonApi.Types[0].Type.Name });
    Console.WriteLine($"{pokemonApi.Name} was caught!");
  }
}