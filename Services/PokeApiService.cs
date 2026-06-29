using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokedex.Services;
static class PokeApiService {
  private record CacheItem(string Data, DateTime CachedAt);
  private static readonly Dictionary<string, CacheItem> cache = new();
  private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);
  private static readonly HttpClient client = new();
  private const string BaseUrl = "https://pokeapi.co/api/v2";

  private static bool CacheIsValid(CacheItem item) {
    return DateTime.UtcNow - item.CachedAt < CacheDuration;
  }

  public static async Task<string> Fetch(string path) {
    string fullUrl = $"{BaseUrl}/{path}";

    if (cache.TryGetValue(fullUrl, out var cacheItem)) {
      if (CacheIsValid(cacheItem)) return cacheItem.Data;
      cache.Remove(fullUrl);
    }

    try {
      HttpResponseMessage response = await client.GetAsync(fullUrl);
      response.EnsureSuccessStatusCode();
      string responseBody = await response.Content.ReadAsStringAsync();
      
      cache[fullUrl] = new CacheItem(responseBody, DateTime.UtcNow);
      return responseBody;
    } catch (HttpRequestException ex) {
      throw new HttpRequestException($"HTTP request failed for '{fullUrl}'", ex);
    } catch (TaskCanceledException ex) {
      throw new TaskCanceledException($"Request timed out for {fullUrl}", ex);
    }
  }
}