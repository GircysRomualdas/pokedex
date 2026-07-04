
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Pokedex.Models.Domain;
using System.Collections.Generic;
using Pokedex.Models.Api;
using System.Linq;

namespace Pokedex.Infrastructure;

public static class PokemonRepository {
  private const string connectionString = "Host=localhost;Port=5432;Database=pokedex;Username=postgres;Password=postgres";

  public static async Task<List<Pokemon>> GetAllAsync() {
    await using var conn = new NpgsqlConnection(connectionString);
    await conn.OpenAsync();

    var pokemons = await conn.QueryAsync<Pokemon>("""
      SELECT  id,
              name,
              height,
              weight,
              base_experience
      FROM pokemons;
    """);

    return pokemons.ToList();
  }

  public static async Task<Pokemon> InsertAsync(Pokemon pokemon) {
    await using var conn = new NpgsqlConnection(connectionString);
    await conn.OpenAsync();

    var id = await conn.ExecuteScalarAsync<int>("""
      INSERT INTO pokemons (name, height, weight, base_experience)
      VALUES (@Name, @Height, @Weight, @BaseExperience)
      RETURNING id;
    """, pokemon);

    pokemon.Id = id;
    return pokemon;
  }

}