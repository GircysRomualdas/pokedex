using Xunit;
using Moq;
using System.Threading.Tasks;

using Pokedex.Infrastructure.Api;
using Pokedex.Services;
using System;

namespace UnitTests.Services;

public class PokemonApiServiceTests {
  // GetPokemonAsync
  [Theory]
  [InlineData("pikachu", 112, 4, 60, "electric")]
  [InlineData("charmander", 62, 6, 85, "fire")]
  [InlineData("squirtle", 63, 5, 90, "water")]
  public async Task GetPokemonAsync_WithExistingName_ReturnPokemon(string name, int baseExperience, int height, int weight, string type) {
    var apiClient = new Mock<IApiClient>();
    var routes = new PokeApiRoutes("https://pokeapi.co/api/v2");
    var url = routes.Pokemon(name);

    apiClient
    .Setup(x => x.FetchAsync(url))
    .ReturnsAsync($$"""
    {
      "name": "{{name}}",
      "base_experience": {{baseExperience}},
      "height": {{height}},
      "weight": {{weight}},
      "types": [
        {
          "type": {
            "name": "{{type}}",
            "url": "https://pokeapi.co/api/v2/type/13/"
          }
        }
      ]
    }
    """);

    var service = new PokemonApiService(apiClient.Object, routes);

    var result = await service.GetPokemonAsync(name);

    Assert.Equal(name, result.Name);
    Assert.Equal(baseExperience, result.BaseExperience);
    Assert.Equal(height, result.Height);
    Assert.Equal(weight, result.Weight);
    Assert.Single(result.Types);

    var pokemonType = result.Types[0].Type;
    Assert.Equal(type, pokemonType.Name);
  }

  [Fact]
  public async Task GetPokemonAsync_Throws() {
    var apiClient = new Mock<IApiClient>();
    var routes = new PokeApiRoutes("https://pokeapi.co/api/v2");
    var url = routes.Pokemon("pikachu");

    apiClient.Setup(x => x.FetchAsync(url)).ThrowsAsync(new Exception());

    var service = new PokemonApiService(apiClient.Object, routes);

    await Assert.ThrowsAsync<Exception>(() => service.GetPokemonAsync("pikachu"));
  }
}