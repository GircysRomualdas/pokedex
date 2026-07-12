using Moq;
using Pokedex.Commands;
using Pokedex.Domain;
using Pokedex.Infrastructure.Api;
using Pokedex.Infrastructure.Repositories;
using Pokedex.Services;
using Pokedex.State;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Commands;

public class MapCommandTests {
  [Fact]
  public async Task Run_WhenMovingNext_PrintsLocationAreas() {
    string url = "https://pokeapi.co/api/v2/location-area?page=2";
    var gameState = new GameState {
      NextLocationUrl = url
    };

    var apiService = new Mock<ILocationAreaApiService>();
    string nextUrl = "https://pokeapi.co/api/v2/location-area?page=3";
    apiService.Setup(x => x.GetPageAsync(gameState.NextLocationUrl))
    .ReturnsAsync(new LocationAreaApi {
      Next = nextUrl,
      Previous = null,
      Results = [
        new LocationAreaApi.LocationAreaResult {Name = "kanto-route-1"},
        new LocationAreaApi.LocationAreaResult {Name = "kanto-route-2"}
      ]
    });

    var locationAreaService = new LocationAreaService(apiService.Object);

    var command = new MapCommand(gameState, locationAreaService);

    using var console = new ConsoleCapture();

    await command.Run(MapDirection.Next);

    apiService.Verify(x => x.GetPageAsync(url), Times.Once);

    Assert.Equal(nextUrl, gameState.NextLocationUrl);

    Assert.Null(gameState.PreviousLocationUrl);

    Assert.Contains("kanto-route-1", console.Output.ToString());
  }
}
