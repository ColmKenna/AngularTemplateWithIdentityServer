// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using System.Collections.Generic;

namespace IdentityServerAspNetIdentity
{
  public static class Config
  {
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("CanViewProducts", new [] { "CanViewProducts" })
        };


    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
                new ApiScope("api1", "My API", new List<string>(){"CanViewProducts"})
        };

    public static IEnumerable<Client> Clients
    {
      get
      {
        string angularip = "https://localhost:5003";
        string blazorip = "https://localhost:5004";

        return new List<Client>
            {
              // machine to machine client
              new Client
              {
                ClientId = "client",
                ClientSecrets = {new Secret("secret".Sha256())},

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // scopes that client has access to
                AllowedScopes = {"api1"}
              },


              // interactive ASP.NET Core MVC client
              new Client
              {
                ClientId = "mvc",
                ClientSecrets = {new Secret("secret".Sha256())},

                AllowedGrantTypes = GrantTypes.Code,

                // where to redirect to after login
                RedirectUris = {"https://localhost:5002/signin-oidc"},

                // where to redirect to after logout
                PostLogoutRedirectUris = {"https://localhost:5002/signout-callback-oidc"},

                AllowedScopes = new List<string>
                {
                  IdentityServerConstants.StandardScopes.OpenId,
                  IdentityServerConstants.StandardScopes.Profile,
                  "api1"
                }
              },
              new Client
              {
                ClientId = "angularClient",
                ClientName = "Angular Client",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                AllowAccessTokensViaBrowser = true,
                RequireConsent = false,

                RedirectUris = new List<string>
                {
                  angularip + "/signin-oidc",
                  angularip + "/redirect-silentrenew",
                  angularip,
                  angularip + "/signin-callback",
                  angularip + "/assets/silent-callback.html"
                },

                PostLogoutRedirectUris = {angularip + "/signout-callback"},
                AllowedCorsOrigins = {angularip},
                AlwaysIncludeUserClaimsInIdToken = true,
                AccessTokenLifetime = 180,

                AllowedScopes =
                {
                  IdentityServerConstants.StandardScopes.OpenId,
                  IdentityServerConstants.StandardScopes.Profile,
                  "productClaims",
                  "customerClaims",
                  "securedApi","api1"
                },
              },
              new Client
              {
                ClientId = "blazorClient",
                ClientName = "Blazor Client",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Code,

                RequirePkce = true,
                RequireConsent = false,

                RedirectUris =
                {
                 blazorip + "/authentication/login-callback"
                },
                PostLogoutRedirectUris =
                {
                  blazorip + "/authentication/logout-callback"
                },

                AllowedCorsOrigins = {blazorip },

                AllowedScopes =
                {
                  IdentityServerConstants.StandardScopes.OpenId,
                  IdentityServerConstants.StandardScopes.Profile,
                  "productClaims",
                  "customerClaims",
                  "api1",
                  "CanViewProducts"
                },
              }

            };
      }
    }
  }
}