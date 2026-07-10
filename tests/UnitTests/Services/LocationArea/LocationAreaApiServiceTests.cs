
using System.Threading.Tasks;
using Moq;
using Pokedex.Infrastructure.Api;
using Pokedex.Services;
using Xunit;

namespace UnitTests.Services;

public class LocationAreaApiServiceTests {
  // GetPageAsync
  [Fact]
  public async Task GetPageAsync_WithExistingUrl_ReturnLocationAreaApi() {
    var apiClient = new Mock<IApiClient>();
    var routes = new PokeApiRoutes("https://pokeapi.co/api/v2");
    var url = routes.Pokemon("canalave-city-area");
    apiClient
    .Setup(x => x.FetchAsync(url))
    .ReturnsAsync("""
    {
      "next": "https://pokeapi.co/api/v2/location-area?page=3",
      "previous": "https://pokeapi.co/api/v2/location-area?page=1",
      "results": [
        {
          "name": "canalave-city-area"
        },
        {
          "name": "kanto-route-1"
        }
      ]
    }
    """);

    var service = new LocationAreaApiService(apiClient.Object, routes);
    var result = await service.GetPageAsync(url);

    Assert.Equal(
        "https://pokeapi.co/api/v2/location-area?page=3",
        result.Next
    );
    Assert.Equal(
        "https://pokeapi.co/api/v2/location-area?page=1",
        result.Previous
    );
    Assert.Equal(2, result.Results.Count);
    Assert.Equal("canalave-city-area", result.Results[0].Name);
    Assert.Equal("kanto-route-1", result.Results[1].Name);
  }

  // GetByNameAsync
  [Fact]
  public async Task GetByNameAsync_WithExistingArea_ReturnLocationAreaDetailApi() {
    var apiClient = new Mock<IApiClient>();
    var routes = new PokeApiRoutes("https://pokeapi.co/api/v2");
    var locationArea = "canalave-city-area";
    apiClient
    .Setup(x => x.FetchAsync(routes.LocationArea(locationArea)))
    .ReturnsAsync("""
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
            "name": "magikarp"
          }
        },
        {
          "pokemon": {
            "name": "tentacool"
          }
        }
      ]
    }
    """);

    var service = new LocationAreaApiService(apiClient.Object, routes);
    var result = await service.GetByNameAsync(locationArea);

    Assert.Equal("canalave-city-area", result.Name);
    Assert.Equal(3, result.PokemonEncounters.Count);
    Assert.Equal(
        "pikachu",
        result.PokemonEncounters[0].Pokemon.Name
    );
    Assert.Equal(
        "magikarp",
        result.PokemonEncounters[1].Pokemon.Name
    );
    Assert.Equal(
        "tentacool",
        result.PokemonEncounters[2].Pokemon.Name
    );
  }

}