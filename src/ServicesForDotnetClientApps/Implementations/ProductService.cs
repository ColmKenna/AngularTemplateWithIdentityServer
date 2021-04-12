using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServicesForDotnetClientApps.Implementations
{
  public class ProductService : IProductService
  {
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
      return await JsonSerializer.DeserializeAsync<IEnumerable<Product>>
        (await _httpClient.GetStreamAsync($"products"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

    }
  }
}