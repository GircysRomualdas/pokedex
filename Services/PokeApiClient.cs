using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokedex.Services;

static class PokeApiClient {
  private static readonly HttpClient client = new();

  public static async Task<string> FetchAsync(string url) {
    if (MemoryCacheService.TryGet(url, out var data)) return data;

    try {
      HttpResponseMessage response = await client.GetAsync(url);
      response.EnsureSuccessStatusCode();
      string responseBody = await response.Content.ReadAsStringAsync();

      MemoryCacheService.Set(url, responseBody);
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