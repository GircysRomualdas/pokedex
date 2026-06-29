using System;
using System.Text.Json;
using System.Threading.Tasks;

using Pokedex.Models.Api;

namespace Pokedex.Services;

static class LocationAreaService {
  private const string Endpoint = "location-area";
  private const string BaseUrl = "https://pokeapi.co/api/v2";
  private const string defaultUrl = $"{BaseUrl}/{Endpoint}";

  static public async Task<LocationAreaApi> GetPageAsync(string? url) {
    string fullUrl = url is null ? defaultUrl : url;
    return await PokeApiSerializer.GetAsync<LocationAreaApi>(fullUrl);
  }

  static public async Task<LocationAreaDetailApi> GetByNameAsync(string locationArea) {
    string fullUrl = $"{defaultUrl}/{locationArea}";
    return await PokeApiSerializer.GetAsync<LocationAreaDetailApi>(fullUrl);
  }
}