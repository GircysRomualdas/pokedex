using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

using Pokedex.Configuration;
using Pokedex.State;
using Pokedex.Commands;
using Pokedex.Services;
using Pokedex.Infrastructure.Repositories;
using Pokedex.Infrastructure.Api;
using Pokedex.Infrastructure.Database;
using Pokedex.Infrastructure.Repositories.Models;
using MongoDB.Driver;

namespace Pokedex;

class Program {
  static async Task Main() {
    var services = new ServiceCollection();
    services.AddSingleton<AppConfiguration>(provider => {
      return new AppConfiguration("appsettings.json");
    });
    services.AddSingleton<MongoDatabase>();
    services.AddSingleton<PokeApiRoutes>(provider => {
      var config = provider.GetRequiredService<AppConfiguration>();
      return new PokeApiRoutes(
        config.PokeApi.BaseUrl
      );
    });
    services.AddSingleton<IMongoCollection<PokemonDocument>>(provider => {
      var db = provider.GetRequiredService<MongoDatabase>();
      return db.GetCollection<PokemonDocument>("pokemon");
    });
    services.AddSingleton<IMongoDatabase>(provider => {
      var mongoDatabase =
          provider.GetRequiredService<MongoDatabase>();

      return mongoDatabase.Database;
    });
    services.AddSingleton<MongoDbHealthCheck>();
    services.AddSingleton<IPokemonRepository, PokemonRepository>();
    services.AddHttpClient();
    services.AddSingleton<IApiClient, ApiClient>();
    services.AddSingleton<IPokemonApiService, PokemonApiService>();
    services.AddSingleton<IPokemonService, PokemonService>();
    services.AddSingleton<ICatchCalculator, CatchCalculator>();
    services.AddSingleton<CatchService>();
    services.AddSingleton<ILocationAreaApiService, LocationAreaApiService>();
    services.AddSingleton<LocationAreaService>();
    services.AddSingleton<GameState>();
    services.AddSingleton<GameInitializer>();
    services.AddSingleton<CatchCommand>();
    services.AddSingleton<ExploreCommand>();
    services.AddSingleton<InspectCommand>();
    services.AddSingleton<MapCommand>();
    services.AddSingleton<PokedexCommand>();

    var provider = services.BuildServiceProvider();

    var healthCheck = provider.GetRequiredService<MongoDbHealthCheck>();
    if (!await healthCheck.CheckAsync()) {
      Console.WriteLine("Could not connect to Pokedex database.");
      return;
    }

    var gameInitializer = provider.GetRequiredService<GameInitializer>();
    await gameInitializer.InitializeAsync();

    var mapCommand = provider.GetRequiredService<MapCommand>();
    var exploreCommand = provider.GetRequiredService<ExploreCommand>();
    var catchCommand = provider.GetRequiredService<CatchCommand>();
    var pokedexCommand = provider.GetRequiredService<PokedexCommand>();
    var inspectCommand = provider.GetRequiredService<InspectCommand>();

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
