using Xunit;
using Moq;

using Pokedex.Domain;
using Pokedex.Infrastructure.Api;
using Pokedex.Infrastructure.Repositories;
using Pokedex.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace UnitTests.Services;

public class PokemonServiceTests {
  // GetPokemonAsync
  [Theory]
  [InlineData("pikachu", 112, 4, 60, "electric")]
  [InlineData("charmander", 62, 6, 85, "fire")]
  [InlineData("squirtle", 63, 5, 90, "water")]
  public async Task GetPokemonAsync_WithExistingName_ReturnPokemon(string name, int baseExperience, int height, int weight, string type) {
    var apiService = new Mock<IPokemonApiService>();
    var repository = new Mock<IPokemonRepository>();

    apiService
    .Setup(x => x.GetPokemonAsync(name))
    .ReturnsAsync(new PokemonApi {
      Name = name,
      BaseExperience = baseExperience,
      Height = height,
      Weight = weight,
      Types = [
        new PokemonApi.TypeInfo {
          Type = new PokemonApi.TypeInfo.TypeData {
            Name = type,
            Url = "https://example.com"
          }
        }
      ]
    });

    var service = new PokemonService(apiService.Object, repository.Object);

    Pokemon result = await service.GetPokemonAsync(name);

    Assert.Equal(name, result.Name);
    Assert.Equal(baseExperience, result.BaseExperience);
    Assert.Equal(height, result.Height);
    Assert.Equal(weight, result.Weight);
    Assert.Contains(type, result.Types);
  }

  [Fact]
  public async Task GetPokemonAsync_WithExistingName_Throws() {
    var apiService = new Mock<IPokemonApiService>();
    var repository = new Mock<IPokemonRepository>();

    apiService
    .Setup(x => x.GetPokemonAsync("pikachu"))
    .ThrowsAsync(new Exception());

    var service = new PokemonService(apiService.Object, repository.Object);

    await Assert.ThrowsAsync<Exception>(() => service.GetPokemonAsync("pikachu"));
  }

  // GetPokemonsAsync
  [Fact]
  public async Task GetPokemonsAsync_ReturnsPokemon() {
    var apiService = new Mock<IPokemonApiService>();
    var repository = new Mock<IPokemonRepository>();

    repository.Setup(x => x.GetPokemonsAsync()).ReturnsAsync(new List<Pokemon> {
      new Pokemon {
        Name = "pikachu",
        BaseExperience = 112,
        Height = 4,
        Weight = 60,
        Types = ["electric"]
      }
    });

    var service = new PokemonService(apiService.Object, repository.Object);

    List<Pokemon> result = await service.GetPokemonsAsync();
    Pokemon pokemon = Assert.Single(result);

    Assert.Equal("pikachu", pokemon.Name);
    Assert.Equal(112, pokemon.BaseExperience);
    Assert.Equal(4, pokemon.Height);
    Assert.Equal(60, pokemon.Weight);
    Assert.Contains("electric", pokemon.Types);
  }

  [Fact]
  public async Task GetPokemonsAsync_ReturnsEmpty() {
    var apiService = new Mock<IPokemonApiService>();
    var repository = new Mock<IPokemonRepository>();

    repository.Setup(x => x.GetPokemonsAsync()).ReturnsAsync([]);

    var service = new PokemonService(apiService.Object, repository.Object);

    List<Pokemon> result = await service.GetPokemonsAsync();
    Assert.Empty(result);
  }

  [Fact]
  public async Task GetPokemonsAsync_Throws() {
    var apiService = new Mock<IPokemonApiService>();
    var repository = new Mock<IPokemonRepository>();

    repository.Setup(x => x.GetPokemonsAsync()).ThrowsAsync(new Exception());

    var service = new PokemonService(apiService.Object, repository.Object);

    await Assert.ThrowsAsync<Exception>(() => service.GetPokemonsAsync());
  }
}