using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Driver;
using Pokedex.Domain;
using Pokedex.Infrastructure.Repositories.Models;

namespace Pokedex.Infrastructure.Repositories;

public class PokemonRepository : IPokemonRepository {
  private readonly IMongoCollection<PokemonDocument> collection;

  public PokemonRepository(IMongoCollection<PokemonDocument> collection) {
    this.collection = collection;
  }

  public async Task InsertPokemonAsync(Pokemon pokemon) {
    PokemonDocument document = PokemonDocumentMapper.ToDocument(pokemon);
    await collection.InsertOneAsync(document);
  }

  public async Task<List<Pokemon>> GetPokemonsAsync() {
    List<PokemonDocument> documents = await collection.Find(_ => true).ToListAsync();
    return documents.Select(p => PokemonDocumentMapper.ToDomain(p)).ToList();
  }
}