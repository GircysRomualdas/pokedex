using System;
using DbUp;

namespace Pokedex.Infrastructure;

static class DatabaseMigrator {
  public static void Run() {
    string connectionString = "Host=localhost;Port=5432;Database=pokedex;Username=postgres;Password=postgres";

    var upgrader =
        DeployChanges.To
            .PostgresqlDatabase(connectionString)
            .WithScriptsFromFileSystem("src/Infrastructure/Scripts")
            .LogToConsole()
            .Build();

    var result = upgrader.PerformUpgrade();

    if (!result.Successful) {
      Console.WriteLine(result.Error);
      throw new Exception("Error running migrations");
    }
  }
}