using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Api.Policies
{

  public static class PolicyBuilder
  {
    public static AuthorizationPolicy Policy(string name, params string[] claims)
    {
      return new AuthorizationPolicyBuilder()
             .RequireAuthenticatedUser()
             .RequireClaim(name, claims)
             .Build();

    }
  }
  public static class CanViewProductsPolicy
  { 
    public const string CanViewProducts = "CanViewProducts";

    public static AuthorizationPolicy Policy() =>
      PolicyBuilder.Policy(CanViewProducts, "true");

  }
}
