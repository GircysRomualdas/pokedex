using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Pokedex.Infrastructure.Api;

static class Cache {
  private record CacheItem(string Data, DateTime CachedAt);
  private static readonly Dictionary<string, CacheItem> _cache = new();
  private static readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);

  private static bool CacheIsValid(CacheItem item) {
    return DateTime.UtcNow - item.CachedAt < _cacheDuration;
  }

  public static bool TryGet(string key, [NotNullWhen(true)] out string? value) {
    value = null;

    if (!_cache.TryGetValue(key, out var item)) return false;

    if (!CacheIsValid(item)) {
      _cache.Remove(key);
      return false;
    }

    value = item.Data;
    return true;
  }

  public static void Set(string key, string value) {
    _cache[key] = new CacheItem(value, DateTime.UtcNow);
  }
}
