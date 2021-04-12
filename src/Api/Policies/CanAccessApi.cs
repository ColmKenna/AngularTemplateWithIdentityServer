using Microsoft.AspNetCore.Authorization;

namespace Api.Policies
{
  public static class CanAccessApi
  {
    public const string ApiScope = "ApiScope";

    public static AuthorizationPolicy Policy() =>
      PolicyBuilder.Policy("scope", "api1");
  }
}