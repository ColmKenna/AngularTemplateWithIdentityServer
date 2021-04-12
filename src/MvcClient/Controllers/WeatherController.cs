using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesForDotnetClientApps;

namespace MvcClient.Controllers
{
  public class WeatherController : Controller
  {
    private IWeatherForcastService weatherForcastService;

    public WeatherController(IWeatherForcastService weatherForcastService)
    {
      this.weatherForcastService = weatherForcastService;
    }
    [Authorize]
    public async Task< IActionResult> Index()
    {
      var forecasts = await this.weatherForcastService.GetWeatherForcast().ConfigureAwait(false);

      return View(forecasts.ToList());
    }
  }
}