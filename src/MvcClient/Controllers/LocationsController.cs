using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesForDotnetClientApps;

namespace MvcClient.Controllers
{
  public class LocationsController : Controller
  {
    private ILocationService locationsService;

    public LocationsController(ILocationService locationsService)
    {
      this.locationsService = locationsService;
    }
    [Authorize]
    public async Task<IActionResult> Index()
    {
      var locations = await this.locationsService.GetLocations().ConfigureAwait(false);

      return View(locations.ToList());
    }
  }
}