using MongoDB.Driver;
using Pokedex.Configuration;

namespace Pokedex.Infrastructure.Database;

public class MongoDatabase {
  private readonly IMongoDatabase _database;
  public IMongoDatabase Database => _database;
  public MongoDatabase(AppConfiguration config) {
    var client = new MongoClient(config.Mongo.ConnectionString);
    _database = client.GetDatabase(config.Mongo.DatabaseName);
  }
  public MongoDatabase(
    string connectionString,
    string databaseName) {
    var client = new MongoClient(connectionString);
    _database = client.GetDatabase(databaseName);
  }

  public IMongoCollection<T> GetCollection<T>(string name) {
    return _database.GetCollection<T>(name);
  }
}