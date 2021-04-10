using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Api.Policies
{

  public static class CanAccessApi
  {
    public const string ApiScope = "ApiScope";

    public static AuthorizationPolicy Policy()
    {
      return new AuthorizationPolicyBuilder()
             .RequireAuthenticatedUser()
             .RequireClaim("scope", "api1")
             .Build();
    }

  }


public static class CanViewProductsPolicy
  { 
    public const string CanViewProducts = "CanViewProducts";

    public static AuthorizationPolicy Policy()
    {
      return new AuthorizationPolicyBuilder()
             .RequireAuthenticatedUser()
             .RequireClaim(CanViewProducts, "true")
             .Build();
    }

  }
}
