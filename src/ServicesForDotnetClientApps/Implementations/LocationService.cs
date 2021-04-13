using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServicesForDotnetClientApps.Implementations
{
  public class LocationService : ILocationService
  {
    private readonly HttpClient _httpClient;

    public LocationService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<IEnumerable<Location>> GetLocations()
    {
      return await JsonSerializer.DeserializeAsync<IEnumerable<Location>>
        (await _httpClient.GetStreamAsync($"locations"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

    }
  }
}