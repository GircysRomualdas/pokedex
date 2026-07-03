using System;
using System.Threading.Tasks;
using Npgsql;

namespace Pokedex.Infrastructure;

static class Database {
  private const string connectionString = "Host=localhost;Port=5432;Database=pokedex;Username=postgres;Password=postgres"; // should I use "appsettings.json" or not?

  public static async Task RunTest() {
    await using var conn = new NpgsqlConnection(connectionString);
    await conn.OpenAsync();

    await using var cmd = new NpgsqlCommand("SELECT version();", conn);

    var result = await cmd.ExecuteScalarAsync();
    string? version = result?.ToString();

    Console.WriteLine(version);
  }
}