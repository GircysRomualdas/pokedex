using Xunit;
using Testcontainers.MongoDb;
using System.Threading.Tasks;
using MongoDB.Driver;
using Pokedex.Infrastructure.Database;
using Pokedex.Infrastructure.Repositories.Models;

namespace IntegrationTests;

public class MongoFixture : IAsyncLifetime {
  public MongoDbContainer Mongo { get; } = new MongoDbBuilder("mongo:8.0").Build();

  public async Task InitializeAsync() {
    await Mongo.StartAsync();
  }

  public async Task DisposeAsync() {
    await Mongo.DisposeAsync();
  }

  public async Task<IMongoCollection<PokemonDocument>> GetCleanPokemonCollectionAsync() {
    var PokemonCollection = new MongoDatabase(Mongo.GetConnectionString(), "pokedex_test").GetCollection<PokemonDocument>("pokemon");
    await PokemonCollection.DeleteManyAsync(_ => true);
    return PokemonCollection;
  }
}

[CollectionDefinition("Mongo")]
public class MongoCollection : ICollectionFixture<MongoFixture> { }