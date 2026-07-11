using MongoDB.Driver;

namespace Pokedex.Infrastructure.Database;

public class MongoDatabase {
  private readonly IMongoDatabase database;
  public IMongoDatabase Database => database;
  public MongoDatabase(string connString, string dbName) {
    var client = new MongoClient(connString);
    database = client.GetDatabase(dbName);
  }

  public IMongoCollection<T> GetCollection<T>(string name) {
    return database.GetCollection<T>(name);
  }
}