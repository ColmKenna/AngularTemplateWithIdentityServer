using System.Collections.Generic;
using Api.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ProductsController : ControllerBase
  {
    IList<Product> prods = new List<Product>()
    {
      new Product(1,"Product 1", 2.3m),
      new Product(2,"Product 2", 1.2m),
      new Product(3,"Product 3", 5.1m),
      new Product(4,"Product 4", 4.6m),
      new Product(5,"Product 5", 1.3m),

    };

    [HttpGet]
    [Authorize(Policy = CanViewProductsPolicy.CanViewProducts)]
    public IActionResult Get()
    {
      return Ok(prods);
    }
  }
}