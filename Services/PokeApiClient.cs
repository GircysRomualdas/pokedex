using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokedex.Services;

static class PokeApiClient {
  private record CacheItem(string Data, DateTime CachedAt);
  private static readonly Dictionary<string, CacheItem> cache = new();
  private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);
  private static readonly HttpClient client = new();

  private static bool CacheIsValid(CacheItem item) {
    return DateTime.UtcNow - item.CachedAt < CacheDuration;
  }

  public static async Task<string> Fetch(string url) {
    if (cache.TryGetValue(url, out var cacheItem)) {
      if (CacheIsValid(cacheItem)) return cacheItem.Data;
      cache.Remove(url);
    }

    try {
      HttpResponseMessage response = await client.GetAsync(url);
      response.EnsureSuccessStatusCode();
      string responseBody = await response.Content.ReadAsStringAsync();

      cache[url] = new CacheItem(responseBody, DateTime.UtcNow);
      return responseBody;
    }
    catch (HttpRequestException ex) {
      throw new HttpRequestException($"HTTP request failed for '{url}'", ex);
    }
    catch (TaskCanceledException ex) {
      throw new TaskCanceledException($"Request timed out for {url}", ex);
    }
  }
}