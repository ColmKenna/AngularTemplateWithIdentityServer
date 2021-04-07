using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorClient.Data;

namespace BlazorClient.Services
{
  public interface IWeatherForcastService  
  {
    Task<IEnumerable<WeatherForecast>> GetWeatherForcast();
  }
}