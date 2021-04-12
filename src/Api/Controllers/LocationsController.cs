using System.Collections.Generic;
using Api.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class LocationsController : ControllerBase
  {
    IList<Location> locations = new List<Location>()
    {
      new Location(1,"Location 1","Address 1"),
      new Location(2,"Location 2", "Address 2"),
      new Location(3,"Location 3", "Address 3"),
      new Location(4,"Location 4", "Address 4"),
      new Location(5,"Location 5", "Address 5"),

    };

    [HttpGet]
    [Authorize(Policy = CanViewLocationsPolicy.CanViewLocations)]
    public IActionResult Get()
    {
      return Ok(locations);
    }
  }
}