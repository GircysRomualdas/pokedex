using System.Net.Http;
using System.Threading.Tasks;

namespace Pokedex.Infrastructure.Api;

public class ApiClient : IApiClient {
  private readonly HttpClient _client;

  public ApiClient(HttpClient client) {
    _client = client;
  }

  public async Task<string> FetchAsync(string url) {
    if (Cache.TryGet(url, out var data)) return data;

    try {
      HttpResponseMessage response = await _client.GetAsync(url);
      response.EnsureSuccessStatusCode();
      string responseBody = await response.Content.ReadAsStringAsync();

      Cache.Set(url, responseBody);
      return responseBody;
    }
    catch (HttpRequestException ex) {
      throw new HttpRequestException($"HTTP request failed for '{url}'", ex);
    }
    catch (TaskCanceledException ex) {
      throw new TaskCanceledException($"Request timed out for '{url}'", ex);
    }
  }
}