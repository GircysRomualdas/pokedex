using System;
using System.Threading.Tasks;
using System.Net.Http;

using Pokedex.Services;

namespace Pokedex.Commands;

static class ExploreCommand {
  public static async Task Run(string[] args) {
    if (args.Length < 2) {
      Console.WriteLine("Wrong number of arguments!");
      Console.WriteLine("usage: explore <location-area>");
      return;
    }

    string locationArea = args[1];
    string path = $"location-area/{locationArea}";
    string responseBody;
    try {
      responseBody = await PokeAPIServices.FetchAsync(path);
    }
    catch (HttpRequestException e) {
        Console.WriteLine($"Network error: {e.Message}");
        return;
    } catch (Exception e) {
        Console.WriteLine($"Unexpected error: {e.Message}");
        return;
    }
    Console.WriteLine($"responseBody: {responseBody}");
  }
}