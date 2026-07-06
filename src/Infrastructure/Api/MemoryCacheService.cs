using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Pokedex.Infrastructure.Api;

static class MemoryCacheService {
  private record CacheItem(string Data, DateTime CachedAt);
  private static readonly Dictionary<string, CacheItem> cache = new();
  private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);

  private static bool CacheIsValid(CacheItem item) {
    return DateTime.UtcNow - item.CachedAt < CacheDuration;
  }

  public static bool TryGet(string key, [NotNullWhen(true)] out string? value) {
    value = null;

    if (!cache.TryGetValue(key, out var item)) return false;

    if (!CacheIsValid(item)) {
      cache.Remove(key);
      return false;
    }

    value = item.Data;
    return true;
  }

  public static void Set(string key, string value) {
    cache[key] = new CacheItem(value, DateTime.UtcNow);
  }
}
