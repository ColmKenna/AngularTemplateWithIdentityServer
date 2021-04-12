using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesForDotnetClientApps
{
  public interface IProductService
  {
    Task<IEnumerable<Product>> GetProducts();
  }

}
