using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesForDotnetClientApps
{
  public interface IWeatherForcastService
  {
    Task<IEnumerable<WeatherForecast>> GetWeatherForcast();
  }
}