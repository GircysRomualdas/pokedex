using System.Threading.Tasks;

namespace Pokedex.Infrastructure.Api;

interface IApiClient {
  Task<string> FetchAsync(string url);
}