using System.Threading.Tasks;
using Moq;
using Pokedex.Infrastructure.Api;
using Pokedex.Services;
using Xunit;
using Pokedex.Commands;
using System.IO;
using System;

namespace IntegrationTests.Commands;

public class ExploreCommandTests {
  private string FetchReturn() {
    return """
    {
      "name": "canalave-city-area",
      "pokemon_encounters": [
        {
          "pokemon": {
            "name": "pikachu"
          }
        },
        {
          "pokemon": {
            "name": "zubat"
          }
        },
        {
          "pokemon": {
            "name": "psyduck"
          }
        }
      ]
    }
    """;
  }

  [Fact]
  public async Task Run_WhenLocationAreaExists_PrintsPokemonEncounters() {
    var apiClient = new Mock<IApiClient>();
    var routes = new PokeApiRoutes("https://pokeapi.co/api/v2");
    string locationArea = "canalave-city-area";
    var url = routes.LocationArea(locationArea);

    apiClient
    .Setup(x => x.FetchAsync(url))
    .ReturnsAsync(FetchReturn());
    var locationAreaApiService = new LocationAreaApiService(apiClient.Object, routes);

    var locationAreaService = new LocationAreaService(locationAreaApiService);
    var exploreCommand = new ExploreCommand(locationAreaService);

    var output = new StringWriter();
    Console.SetOut(output);
    await exploreCommand.Run(["explore", locationArea]);

    Assert.Contains("pikachu", output.ToString());
    Assert.Contains("zubat", output.ToString());
    Assert.Contains("psyduck", output.ToString());
  }

  [Fact]
  public async Task Run_WhenArgumentsAreInvalid_PrintsUsage() {
    var apiClient = new Mock<IApiClient>();
    var routes = new PokeApiRoutes("https://pokeapi.co/api/v2");
    string locationArea = "canalave-city-area";
    var url = routes.LocationArea(locationArea);

    apiClient
    .Setup(x => x.FetchAsync(url))
    .ReturnsAsync(FetchReturn());
    var locationAreaApiService = new LocationAreaApiService(apiClient.Object, routes);

    var locationAreaService = new LocationAreaService(locationAreaApiService);
    var exploreCommand = new ExploreCommand(locationAreaService);

    var output = new StringWriter();
    Console.SetOut(output);
    await exploreCommand.Run(["explore"]);

    Assert.Contains("Wrong number of arguments", output.ToString());
  }
}
