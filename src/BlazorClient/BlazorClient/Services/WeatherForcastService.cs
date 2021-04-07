using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorClient.Data;

namespace BlazorClient.Services
{
  public class WeatherForcastService: IWeatherForcastService
  {
    private readonly HttpClient _httpClient;

    public WeatherForcastService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForcast()
    {
      return await JsonSerializer.DeserializeAsync<IEnumerable<WeatherForecast>>
        (await _httpClient.GetStreamAsync($"weatherforecast"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }
  }
}
