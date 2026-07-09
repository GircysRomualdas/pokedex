using System.Threading.Tasks;
using Pokedex.Infrastructure.Api;

namespace Pokedex.Services;

public interface ILocationAreaApiService {
  Task<LocationAreaApi> GetPageAsync(string? url);
  Task<LocationAreaDetailApi> GetByNameAsync(string locationArea);
}