using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Pokedex.Models.Domain;

namespace Pokedex.Infrastructure;

static class PokemonRepository {
  private const string connString = "mongodb://admin:pass123@localhost:27017";
  private static readonly MongoClient client = new MongoClient(connString);
  private static readonly IMongoDatabase database = client.GetDatabase("PokedexDb");
  private static readonly IMongoCollection<Pokemon> collection = database.GetCollection<Pokemon>("pokemon");

  public static async Task<Pokemon> InsertPokemonAsync(Pokemon pokemon) {
    await collection.InsertOneAsync(pokemon);
    return pokemon;
  }

  public static Task<List<Pokemon>> GetPokemonsAsync() {
    return collection.Find(_ => true).ToListAsync();
  }
}