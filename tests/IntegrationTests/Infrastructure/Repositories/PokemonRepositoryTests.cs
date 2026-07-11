using Xunit;
using Pokedex.Infrastructure.Database;
using Pokedex.Infrastructure.Repositories;
using Pokedex.Infrastructure.Repositories.Models;
using System.Threading.Tasks;
using Pokedex.Domain;
using MongoDB.Driver;

namespace IntegrationTests.Repositories;

public class PokemonRepositoryTests : IClassFixture<MongoFixture> {
  private readonly MongoFixture fixture;

  public PokemonRepositoryTests(MongoFixture fixture) {
    this.fixture = fixture;
  }

  private IMongoCollection<PokemonDocument> GetPokemonCollection() {
    var db = new MongoDatabase(fixture.Mongo.GetConnectionString(), "pokedex_test");
    return db.GetCollection<PokemonDocument>("pokemon");
  }

  [Fact]
  public async Task InsertPokemonAsync_InsertsPokemonDocument() {
    var collection = GetPokemonCollection();
    await collection.DeleteManyAsync(_ => true);

    var repository = new PokemonRepository(collection);

    var pokemon = new Pokemon {
      Name = "pikachu",
      Types = ["electric"],
      Height = 4,
      Weight = 60,
      BaseExperience = 112
    };

    await repository.InsertPokemonAsync(pokemon);

    var documents = await collection.Find(_ => true).ToListAsync();

    Assert.Single(documents);
    Assert.Equal("pikachu", documents[0].Name);
    Assert.Equal(112, documents[0].BaseExperience);
    Assert.Equal(4, documents[0].Height);
    Assert.Equal(60, documents[0].Weight);
    Assert.Contains("electric", documents[0].Types);
  }

  [Fact]
  public async Task GetPokemonsAsync_ReturnsPokemons() {
    var collection = GetPokemonCollection();
    await collection.DeleteManyAsync(_ => true);

    var repository = new PokemonRepository(collection);
    var pokemon = new Pokemon {
      Name = "pikachu",
      Types = ["electric"],
      Height = 4,
      Weight = 60,
      BaseExperience = 112
    };

    var document = PokemonDocumentMapper.ToDocument(pokemon);
    await collection.InsertOneAsync(document);

    var result = await repository.GetPokemonsAsync();

    Assert.Single(result);
    Assert.Equal("pikachu", result[0].Name);
    Assert.Equal(112, result[0].BaseExperience);
    Assert.Equal(4, result[0].Height);
    Assert.Equal(60, result[0].Weight);
    Assert.Contains("electric", result[0].Types);
  }

  [Fact]
  public async Task GetPokemonsAsync_WhenDatabaseEmpty_ReturnsEmptyList() {
    var collection = GetPokemonCollection();
    await collection.DeleteManyAsync(_ => true);
    var repository = new PokemonRepository(collection);
    var result = await repository.GetPokemonsAsync();
    Assert.Empty(result);
  }
}
