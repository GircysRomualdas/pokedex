using System;
using Npgsql;

namespace Pokedex.Infrastructure;

static class Database {
  private const string connectionString = "Host=localhost;Port=5432;Database=pokedex;Username=postgres;Password=postgres";

  public static void runTest() {
    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    var cmd = new NpgsqlCommand("SELECT version();", connection);

    string? version = (string?)cmd.ExecuteScalar();

    Console.WriteLine(version);
  }
}