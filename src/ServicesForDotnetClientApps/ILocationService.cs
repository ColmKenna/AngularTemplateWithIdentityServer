using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesForDotnetClientApps
{
  public interface ILocationService
  {
    Task<IEnumerable<Location>> GetLocations();
  }
}