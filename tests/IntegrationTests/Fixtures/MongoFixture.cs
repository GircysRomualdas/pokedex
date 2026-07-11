using Xunit;
using Testcontainers.MongoDb;
using System.Threading.Tasks;

namespace IntegrationTests;

public class MongoFixture : IAsyncLifetime {
  public MongoDbContainer Mongo { get; } = new MongoDbBuilder("mongo:8.0").Build();

  public async Task InitializeAsync() {
    await Mongo.StartAsync();
  }

  public async Task DisposeAsync() {
    await Mongo.DisposeAsync();
  }
}