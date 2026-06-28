using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokedex.Services;
static class PokeAPIServices {
  static HttpClient client = new HttpClient();
  static string baseUrl = "https://pokeapi.co/api/v2";
  public static async Task<string> PokeAPI(string path) {
    string fullUrl = $"{baseUrl}/{path}";

    HttpResponseMessage response = await client.GetAsync(fullUrl);
    response.EnsureSuccessStatusCode();

    string responseBody = await response.Content.ReadAsStringAsync();
    return responseBody;
  }
}