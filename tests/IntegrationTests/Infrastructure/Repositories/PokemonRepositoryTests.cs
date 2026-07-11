using Xunit;
using Pokedex.Infrastructure.Database;
using Pokedex.Infrastructure.Repositories;
using Pokedex.Infrastructure.Repositories.Models;
using System.Threading.Tasks;
using Pokedex.Domain;
using MongoDB.Driver;

namespace IntegrationTests.Repositories;

[Collection("Mongo")]
public class PokemonRepositoryTests {
  private readonly MongoFixture fixture;

  public PokemonRepositoryTests(MongoFixture fixture) {
    this.fixture = fixture;
  }

  private static Pokemon CreatePikachu() {
    return new Pokemon {
      Name = "pikachu",
      Types = ["electric"],
      Height = 4,
      Weight = 60,
      BaseExperience = 112
    };
  }

  private IMongoCollection<PokemonDocument> GetPokemonCollection() {
    var db = new MongoDatabase(fixture.Mongo.GetConnectionString(), "pokedex_test");
    return db.GetCollection<PokemonDocument>("pokemon");
  }

  [Fact]
  public async Task InsertPokemonAsync_InsertsPokemonDocument() {
    var collection = await fixture.GetCleanPokemonCollectionAsync();

    var repository = new PokemonRepository(collection);

    var pokemon = CreatePikachu();

    await repository.InsertPokemonAsync(pokemon);

    var documents = await collection.Find(_ => true).ToListAsync();

    Assert.Single(documents);
    Assert.Equal(pokemon.Name, documents[0].Name);
    Assert.Equal(pokemon.BaseExperience, documents[0].BaseExperience);
    Assert.Equal(pokemon.Height, documents[0].Height);
    Assert.Equal(pokemon.Weight, documents[0].Weight);
  }

  [Fact]
  public async Task GetPokemonsAsync_ReturnsPokemons() {
    var collection = GetPokemonCollection();
    await collection.DeleteManyAsync(_ => true);

    var repository = new PokemonRepository(collection);
    var pokemon = CreatePikachu();

    var document = PokemonDocumentMapper.ToDocument(pokemon);
    await collection.InsertOneAsync(document);

    var result = await repository.GetPokemonsAsync();

    Assert.Single(result);
    Assert.Equal(pokemon.Name, result[0].Name);
    Assert.Equal(pokemon.BaseExperience, result[0].BaseExperience);
    Assert.Equal(pokemon.Height, result[0].Height);
    Assert.Equal(pokemon.Weight, result[0].Weight);
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
