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
}