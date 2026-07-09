using System.Threading.Tasks;

namespace Pokedex.Infrastructure.Api;

public interface IApiClient {
  Task<string> FetchAsync(string url);
}