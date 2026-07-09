using Xunit;
using System.Threading.Tasks;
using Moq;
using Pokedex.Services;
using Pokedex.Infrastructure.Api;
using Pokedex.Domain;

namespace UnitTests.Services;

public class LocationAreaServiceTests {
  // GetLocationAreaAsync
  [Fact]
  public async Task GetLocationAreaAsync_WithExistingUrl_ReturnLocationArea() {
    var apiService = new Mock<ILocationAreaApiService>();
    string url = "https://pokeapi.co/api/v2/location-area?page=2";

    apiService.Setup(x => x.GetPageAsync(url)).ReturnsAsync(new LocationAreaApi {
      Next = "https://pokeapi.co/api/v2/location-area?page=2",
      Previous = null,
      Results = [
        new LocationAreaApi.LocationAreaResult {Name = "kanto-route-1"},
        new LocationAreaApi.LocationAreaResult {Name = "kanto-route-2"}
      ]
    });

    var service = new LocationAreaService(apiService.Object);
    var result = await service.GetLocationAreaAsync(url);

    Assert.Equal("https://pokeapi.co/api/v2/location-area?page=2", result.Next);

    Assert.Equal(2, result.Areas.Count);
    Assert.Contains("kanto-route-1", result.Areas);
    Assert.Contains("kanto-route-2", result.Areas);
  }

  [Fact]
  public async Task GetLocationAreaAsync_WithEmptyResults_ReturnsEmptyAreas() {
    var apiService = new Mock<ILocationAreaApiService>();

    apiService
        .Setup(x => x.GetPageAsync(null))
        .ReturnsAsync(new LocationAreaApi());

    var service = new LocationAreaService(apiService.Object);

    LocationArea result =
        await service.GetLocationAreaAsync(null);

    Assert.Empty(result.Areas);
  }

  // GetLocationAreaDetailAsync
  [Fact]
  public async Task GetLocationAreaDetailAsync_WithExistingArea_ReturnLocationArea() {
    var apiService = new Mock<ILocationAreaApiService>();
    string locationArea = "canalave-city-area";

    apiService.Setup(x => x.GetByNameAsync(locationArea)).ReturnsAsync(new LocationAreaDetailApi {
      Name = "canalave-city-area",
      PokemonEncounters = [
        new LocationAreaDetailApi.PokemonEncounter {
          Pokemon = new LocationAreaDetailApi.Pokemon {Name = "pikachu"}
        },
        new LocationAreaDetailApi.PokemonEncounter {
          Pokemon = new LocationAreaDetailApi.Pokemon {Name = "magikarp"}
        }
      ]
    });

    var service = new LocationAreaService(apiService.Object);
    LocationAreaDetail result = await service.GetLocationAreaDetailAsync(locationArea);

    Assert.Equal("canalave-city-area", result.Name);
    Assert.Equal(2, result.PokemonEncounters.Count);
    Assert.Contains("pikachu", result.PokemonEncounters);
    Assert.Contains("magikarp", result.PokemonEncounters);
  }
}
