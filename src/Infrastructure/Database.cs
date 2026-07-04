using System;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Pokedex.Models.Domain;

namespace Pokedex.Infrastructure;

static class Database {
  private const string connectionString = "Host=localhost;Port=5432;Database=pokedex;Username=postgres;Password=postgres"; // should I use "appsettings.json" or not?

  public static async Task RunTest() {
    await using var conn = new NpgsqlConnection(connectionString);
    await conn.OpenAsync();

    var version = await conn.ExecuteScalarAsync<string>("SELECT version();", conn);

    Console.WriteLine(version);
  }

  public static async Task GetPokemons() {
    await using var conn = new NpgsqlConnection(connectionString);
    await conn.OpenAsync();

    var pokemons = await conn.QueryAsync<string>("SELECT name FROM pokemons;");

    foreach (var name in pokemons) {
      Console.WriteLine($"Pokemon: {name}");
    }
  }

  public static async Task InsertPokemon(Pokemon pokemon) {
    await using var conn = new NpgsqlConnection(connectionString);
    await conn.OpenAsync();

    var id = await conn.ExecuteScalarAsync<int>("""
      INSERT INTO pokemons (name, height, weight, base_experience)
      VALUES (@Name, @Height, @Weight, @BaseExperience)
      RETURNING id;
    """, pokemon);

    Console.WriteLine($"Inserted pokemon with id: {id}");
    // should I add one by one or insert all pokemons at once?
  }

  public static async Task DeletePokemons() {
    await using var conn = new NpgsqlConnection(connectionString);
    await conn.OpenAsync();

    int rows = await conn.ExecuteAsync("DELETE FROM pokemons;");

    Console.WriteLine($"Rows deleted {rows}");
  }
}