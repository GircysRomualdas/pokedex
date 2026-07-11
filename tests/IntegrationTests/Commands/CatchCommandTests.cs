using System.Threading.Tasks;
using MongoDB.Driver;
using Xunit;
using Moq;

using Pokedex.State;
using Pokedex.Commands;
using Pokedex.Services;
using Pokedex.Infrastructure.Repositories;
using Pokedex.Infrastructure.Api;
using Pokedex.Infrastructure.Database;
using Pokedex.Infrastructure.Repositories.Models;
using Pokedex.Domain;
using System.IO;
using System;

namespace IntegrationTests.Commands;

public class CatchCommandTests : IClassFixture<MongoFixture> {
  private readonly MongoFixture fixture;

  public CatchCommandTests(MongoFixture fixture) {
    this.fixture = fixture;
  }

  [Fact]
  public async Task Run_CatchCommand_WhenPokemonCaught_RegistersPokemon() {
    var db = new MongoDatabase(fixture.Mongo.GetConnectionString(), "pokedex_test");
    var collection = db.GetCollection<PokemonDocument>("pokemon");
    await collection.DeleteManyAsync(_ => true);
    var pokemonRepository = new PokemonRepository(collection);

    var calculator = new Mock<ICatchCalculator>();
    calculator
    .Setup(x => x.CanCatch(It.IsAny<Pokemon>()))
    .Returns(true);

    var pokemonApiService = new Mock<IPokemonApiService>();
    pokemonApiService.Setup(x => x.GetPokemonAsync("pikachu"))
    .ReturnsAsync(new PokemonApi {
      Name = "pikachu",
      BaseExperience = 112,
      Height = 4,
      Weight = 60,
      Types = [
        new PokemonApi.TypeInfo {
          Type = new PokemonApi.TypeInfo.TypeData {
            Name = "electric",
            Url = "https://example.com"
          }
        }
      ]
    });

    var pokemonService = new PokemonService(pokemonApiService.Object, pokemonRepository);
    var catchCalculator = new CatchCalculator();
    var catchService = new CatchService(pokemonService, pokemonRepository, calculator.Object);
    var gameState = new GameState {
      Pokedex = await pokemonService.GetPokemonsAsync()
    };
    var catchCommand = new CatchCommand(catchService, gameState);

    var output = new StringWriter();
    Console.SetOut(output);

    await catchCommand.Run(["catch", "pikachu"]);

    Assert.Contains(gameState.Pokedex, p => p.Name == "pikachu");

    var documents = await collection.Find(_ => true).ToListAsync();
    Assert.Single(documents);
    Assert.Equal("pikachu", documents[0].Name);

    Assert.Contains("pikachu was caught!", output.ToString());
  }

  [Fact]
  public async Task Run_CatchPokemon_WhenEscaped_DoesNotRegisterPokemon() {
    var db = new MongoDatabase(fixture.Mongo.GetConnectionString(), "pokedex_test");
    var collection = db.GetCollection<PokemonDocument>("pokemon");
    await collection.DeleteManyAsync(_ => true);
    var pokemonRepository = new PokemonRepository(collection);

    var calculator = new Mock<ICatchCalculator>();
    calculator
    .Setup(x => x.CanCatch(It.IsAny<Pokemon>()))
    .Returns(false);

    var pokemonApiService = new Mock<IPokemonApiService>();
    pokemonApiService.Setup(x => x.GetPokemonAsync("pikachu"))
    .ReturnsAsync(new PokemonApi {
      Name = "pikachu",
      BaseExperience = 112,
      Height = 4,
      Weight = 60,
      Types = [
        new PokemonApi.TypeInfo {
          Type = new PokemonApi.TypeInfo.TypeData {
            Name = "electric",
            Url = "https://example.com"
          }
        }
      ]
    });

    var pokemonService = new PokemonService(pokemonApiService.Object, pokemonRepository);
    var catchCalculator = new CatchCalculator();
    var catchService = new CatchService(pokemonService, pokemonRepository, calculator.Object);
    var gameState = new GameState {
      Pokedex = await pokemonService.GetPokemonsAsync()
    };
    var catchCommand = new CatchCommand(catchService, gameState);

    var output = new StringWriter();
    Console.SetOut(output);

    await catchCommand.Run(["catch", "pikachu"]);

    Assert.DoesNotContain(gameState.Pokedex, p => p.Name == "pikachu");

    var documents = await collection.Find(_ => true).ToListAsync();
    Assert.Empty(documents);

    Assert.Contains("pikachu escaped!", output.ToString());
  }
}
