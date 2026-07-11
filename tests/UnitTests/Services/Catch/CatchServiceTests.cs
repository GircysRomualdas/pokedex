
using System.Threading.Tasks;
using Moq;
using Pokedex.Domain;
using Pokedex.Infrastructure.Repositories;
using Pokedex.Services;
using Xunit;

namespace UnitTests.Services;

public class CatchServiceTests {
  [Theory]
  [InlineData("pikachu")]
  [InlineData("charmander")]
  [InlineData("squirtle")]
  public async Task CatchPokemonAsync_WhenCatchSucceeds_ReturnsCaughtPokemon(string name) {
    var pokemonService = new Mock<IPokemonService>();
    var repository = new Mock<IPokemonRepository>();
    var calculator = new Mock<ICatchCalculator>();

    var pokemon = new Pokemon {
      Name = name,
      BaseExperience = 10,
      Height = 4,
      Weight = 60
    };

    calculator
    .Setup(x => x.CanCatch(pokemon))
    .Returns(true);

    pokemonService.Setup(x => x.GetPokemonAsync(name)).ReturnsAsync(pokemon);

    repository.Setup(x => x.InsertPokemonAsync(pokemon));

    var service = new CatchService(pokemonService.Object, repository.Object, calculator.Object);
    var result = await service.CatchPokemonAsync(name);

    Assert.True(result.IsCaught);
    Assert.Equal(name, result.Pokemon.Name);

    repository.Verify(
        x => x.InsertPokemonAsync(pokemon),
        Times.Once
    );
  }

  [Fact]
  public async Task CatchPokemonAsync_WhenCatchFails_DoesNotSavePokemon() {
    var pokemonService = new Mock<IPokemonService>();
    var repository = new Mock<IPokemonRepository>();
    var calculator = new Mock<ICatchCalculator>();

    var pokemon = new Pokemon {
      Name = "pikachu",
      BaseExperience = 112,
      Height = 4,
      Weight = 60
    };

    pokemonService
        .Setup(x => x.GetPokemonAsync("pikachu"))
        .ReturnsAsync(pokemon);

    calculator
        .Setup(x => x.CanCatch(pokemon))
        .Returns(false);

    var service = new CatchService(
        pokemonService.Object,
        repository.Object,
        calculator.Object
    );

    var result = await service.CatchPokemonAsync("pikachu");

    Assert.False(result.IsCaught);

    repository.Verify(
        x => x.InsertPokemonAsync(It.IsAny<Pokemon>()),
        Times.Never
    );
  }
}
