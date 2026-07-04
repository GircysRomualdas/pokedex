
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Pokedex.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Pokedex.Infrastructure;

public static class PokemonRepository {
  private const string connectionString = "Host=localhost;Port=5432;Database=pokedex;Username=postgres;Password=postgres";

  public static async Task<List<Pokemon>> GetAllAsync() {
    await using var conn = new NpgsqlConnection(connectionString);
    await conn.OpenAsync();

    var rows = await conn.QueryAsync<PokemonRow>("""
      SELECT id, name, types, height, weight, base_experience
      FROM pokemons;
    """);

    var result = rows.Select(r => new Pokemon {
      Id = r.id,
      Name = r.name,
      Types = r.types.Split(',').ToList(),
      Height = r.height,
      Weight = r.weight,
      BaseExperience = r.base_experience
    }).ToList();

    return result;
  }

  public static async Task<Pokemon> InsertAsync(Pokemon pokemon) {
    await using var conn = new NpgsqlConnection(connectionString);
    await conn.OpenAsync();

    var id = await conn.ExecuteScalarAsync<int>("""
      INSERT INTO pokemons (name, types, height, weight, base_experience)
      VALUES (@Name, @Types, @Height, @Weight, @BaseExperience)
      RETURNING id;
    """, new {
      pokemon.Name,
      Types = string.Join(",", pokemon.Types),
      pokemon.Height,
      pokemon.Weight,
      pokemon.BaseExperience
    });

    pokemon.Id = id;
    return pokemon;
  }

}