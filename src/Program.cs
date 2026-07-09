using System;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

using Pokedex.Configuration;
using Pokedex.State;
using Pokedex.Commands;
using Pokedex.Services;
using Pokedex.Infrastructure.Repositories;
using Pokedex.Infrastructure.Api;
using Pokedex.Infrastructure.Database;
using Pokedex.Infrastructure.Repositories.Models;

namespace Pokedex;

class Program {
  static async Task Main() {
    var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
    var mongoOptions = configuration.GetSection("Mongo").Get<MongoOptions>() ?? throw new Exception("Mongo configuration is missing");
    var pokeApiOptions = configuration.GetSection("PokeApi").Get<PokeApiOptions>() ?? throw new Exception("PokeApi configuration is missing");

    var mongoDatabase = new MongoDatabase(
      mongoOptions.ConnectionString,
      mongoOptions.DatabaseName
    );

    var healthCheck = new MongoDbHealthCheck(mongoDatabase.Database);
    if (!await healthCheck.CheckAsync()) {
      Console.WriteLine("Could not connect to Pokedex database.");
      return;
    }

    var pokemonApiService = new PokeApiRoutes(pokeApiOptions.BaseUrl);
    var pokemonRepository = new PokemonRepository(mongoDatabase.GetCollection<PokemonDocument>("pokemon"));
    var apiClient = new ApiClient(new HttpClient());
    var pokemonService = new PokemonService(new PokemonApiService(apiClient, pokemonApiService), pokemonRepository);
    var catchService = new CatchService(pokemonService, pokemonRepository);
    var locationAreaService = new LocationAreaService(new LocationAreaApiService(apiClient, pokemonApiService));
    var gameState = new GameState {
      Pokedex = await pokemonService.GetPokemonsAsync()
    };
    var catchCommand = new CatchCommand(catchService, gameState);
    var exploreCommand = new ExploreCommand(locationAreaService);
    var inspectCommand = new InspectCommand(gameState);
    var mapCommand = new MapCommand(gameState, locationAreaService);
    var pokedexCommand = new PokedexCommand(gameState);

    Console.WriteLine("Welcome to the Pokedex!");
    HelpCommand.Run();

    while (true) {
      Console.Write("> ");
      string? input = Console.ReadLine();
      if (string.IsNullOrWhiteSpace(input)) continue;

      string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

      switch (parts[0]) {
        case "exit":
          ExitCommand.Run();
          break;
        case "help":
          HelpCommand.Run();
          break;
        case "map":
          await mapCommand.Run(MapDirection.Next);
          break;
        case "mapb":
          await mapCommand.Run(MapDirection.Previous);
          break;
        case "explore":
          await exploreCommand.Run(parts);
          break;
        case "catch":
          await catchCommand.Run(parts);
          break;
        case "pokedex":
          pokedexCommand.Run();
          break;
        case "inspect":
          inspectCommand.Run(parts);
          break;
        default:
          Console.WriteLine("Unknown command");
          break;
      }
    }
  }
}
