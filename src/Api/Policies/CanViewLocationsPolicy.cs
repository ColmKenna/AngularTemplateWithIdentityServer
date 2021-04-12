using Microsoft.AspNetCore.Authorization;

namespace Api.Policies
{
  public static class CanViewLocationsPolicy
  {
    public const string CanViewLocations = "CanViewLocations";

    public static AuthorizationPolicy Policy() =>
      PolicyBuilder.Policy(CanViewLocations, "true");

  }
}