using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Driver;
using Pokedex.Domain;
using Pokedex.Infrastructure.Repositories.Models;

namespace Pokedex.Infrastructure.Repositories;

static class PokemonRepository {
  private const string connString = "mongodb://admin:pass123@localhost:27017";
  private static readonly MongoClient client = new MongoClient(connString);
  private static readonly IMongoDatabase database = client.GetDatabase("PokedexDb");
  private static readonly IMongoCollection<PokemonDocument> collection = database.GetCollection<PokemonDocument>("pokemon");

  public static async Task<bool> CheckConnectionAsync() {
    try {
      await client.ListDatabaseNamesAsync();
      return true;
    }
    catch (MongoException) {
      return false;
    }
  }

  public static async Task<Pokemon> InsertPokemonAsync(Pokemon pokemon) {
    PokemonDocument document = PokemonDocumentMapper.ToDocument(pokemon);
    await collection.InsertOneAsync(document);
    return pokemon;
  }

  public static async Task<List<Pokemon>> GetPokemonsAsync() {
    List<PokemonDocument> documents = await collection.Find(_ => true).ToListAsync();
    return documents.Select(p => PokemonDocumentMapper.ToDomain(p)).ToList();
  }
}
