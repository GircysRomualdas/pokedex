using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Pokedex.Infrastructure.Database;

class MongoDbHealthCheck {
  private readonly IMongoDatabase _database;
  public MongoDbHealthCheck(IMongoDatabase database) {
    _database = database;
  }

  public async Task<bool> CheckAsync() {
    try {
      await _database.RunCommandAsync<BsonDocument>(new BsonDocument("ping", 1));
      return true;
    }
    catch (MongoException) {
      return false;
    }
  }
}