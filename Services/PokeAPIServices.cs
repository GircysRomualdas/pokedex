using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokedex.Services;
static class PokeAPIServices {
  private record CacheItem(string Data, DateTime CachedAt);
  private static readonly Dictionary<string, CacheItem> cache = new();
  private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);
  private static readonly HttpClient client = new();
  private const string BaseUrl = "https://pokeapi.co/api/v2";

  private static bool CacheIsValid(CacheItem item) {
    return DateTime.UtcNow - item.CachedAt < CacheDuration;
  }

  public static async Task<string> FetchAsync(string path) {
    string fullUrl = $"{BaseUrl}/{path}";
    Console.WriteLine($"fullUrl: {fullUrl}"); // temp for test

    if (cache.TryGetValue(fullUrl, out var cacheItem)) {
      if (CacheIsValid(cacheItem)) return cacheItem.Data;
      cache.Remove(fullUrl);
    }

    HttpResponseMessage response = await client.GetAsync(fullUrl);
    response.EnsureSuccessStatusCode();

    string responseBody = await response.Content.ReadAsStringAsync();
    
    cache[fullUrl] = new CacheItem(responseBody, DateTime.UtcNow);
    return responseBody;
  }
}