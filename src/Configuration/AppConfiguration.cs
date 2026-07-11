using System;
using Microsoft.Extensions.Configuration;

namespace Pokedex.Configuration;

public class AppConfiguration {
  public MongoOptions Mongo { get; }
  public PokeApiOptions PokeApi { get; }

  public AppConfiguration(string fileName) {
    var configuration = new ConfigurationBuilder().AddJsonFile(fileName, optional: false).Build();

    Mongo = configuration.GetSection("Mongo").Get<MongoOptions>() ?? throw new Exception("Mongo configuration is missing");
    PokeApi = configuration.GetSection("PokeApi").Get<PokeApiOptions>() ?? throw new Exception("PokeApi configuration is missing");
  }
}