using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Pokedex.Infrastructure.Database;

class MongoDbHealthCheck {
  private readonly IMongoDatabase database;
  public MongoDbHealthCheck(IMongoDatabase database) {
    this.database = database;
  }

  public async Task<bool> CheckAsync() {
    try {
      await database.RunCommandAsync<BsonDocument>(new BsonDocument("ping", 1));
      return true;
    }
    catch (MongoException) {
      return false;
    }
  }
}