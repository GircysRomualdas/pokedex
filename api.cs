using System.Net.Http;
using System.Threading.Tasks;

namespace pokedex;
static class Api {
  static HttpClient client = new HttpClient();
  public static async Task ApiPoke() {
    string url = "https://pokeapi.co/api/v2";

    HttpResponseMessage response = await client.GetAsync(url);
    response.EnsureSuccessStatusCode();

    string responseBody = await response.Content.ReadAsStringAsync();
    Console.WriteLine(responseBody);
  }
}