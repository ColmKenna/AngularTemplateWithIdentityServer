// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Security.Claims;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using IdentityModel;
using IdentityServerAspNetIdentity.Data;
using IdentityServerHost.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace IdentityServerAspNetIdentity
{
  public class SeedData
  {

    private static void SeedConfigData(IServiceScopeFactory serviceScopeFactory)
    {
      using (var serviceScope = serviceScopeFactory.CreateScope())
      {
        serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

        var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        context.Database.Migrate();
        foreach (var client in Config.Clients)
        {
          if (!context.Clients.Any(x => x.ClientId == client.ClientId))
            context.Clients.Add(client.ToEntity());
        }
        context.SaveChanges();

        foreach (var resource in Config.IdentityResources)
        {
          if (!context.IdentityResources.Any(x => x.Name == resource.Name))
            context.IdentityResources.Add(resource.ToEntity());
        }
        context.SaveChanges();

        foreach (var resource in Config.ApiScopes)
        {
          if (!context.ApiScopes.Any(x => x.Name == resource.Name))
            context.ApiScopes.Add(resource.ToEntity());
        }
        context.SaveChanges();
      }
    }
    public static void EnsureSeedData(IServiceProvider serviceProvider)
    {

      {
        var servicesScopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
        SeedUsersInDataBase(servicesScopeFactory);
        SeedConfigData(servicesScopeFactory);

      }
    }

    private static void SeedUsersInDataBase(IServiceScopeFactory servicesScopeFactory)
    {
      using (var scope = servicesScopeFactory.CreateScope())
      {
        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
        context.Database.Migrate();

        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var alice = userMgr.FindByNameAsync("alice").Result;
        if (alice == null)
        {
          alice = new ApplicationUser
          {
            UserName = "alice",
            Email = "AliceSmith@email.com",
            EmailConfirmed = true,
          };
          IdentityResult result = userMgr.CreateAsync(alice, "Pass123$").Result;
          if (!result.Succeeded)
          {
            throw new Exception(result.Errors.First().Description);
          }
          result = userMgr.AddClaimsAsync(alice, new Claim[]
          {
            new Claim(JwtClaimTypes.Name, "Alice Smith"),
            new Claim(JwtClaimTypes.GivenName, "Alice"),
            new Claim(JwtClaimTypes.FamilyName, "Smith"),
            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
            new Claim("CanViewProducts", "true"),
            new Claim("CanViewLocations", "true"),
            

          }).Result;
          if (!result.Succeeded)
          {
            throw new Exception(result.Errors.First().Description);
          }

          Log.Debug("alice created");
        }
        else
        {
          Log.Debug("alice already exists");
        }

        var bob = userMgr.FindByNameAsync("bob").Result;
        if (bob == null)
        {
          bob = new ApplicationUser
          {
            UserName = "bob",
            Email = "BobSmith@email.com",
            EmailConfirmed = true
          };
          var result = userMgr.CreateAsync(bob, "Pass123$").Result;
          if (!result.Succeeded)
          {
            throw new Exception(result.Errors.First().Description);
          }

          result = userMgr.AddClaimsAsync(bob, new Claim[]
          {
            new Claim(JwtClaimTypes.Name, "Bob Smith"),
            new Claim(JwtClaimTypes.GivenName, "Bob"),
            new Claim(JwtClaimTypes.FamilyName, "Smith"),
            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
            new Claim("location", "somewhere")
          }).Result;
          if (!result.Succeeded)
          {
            throw new Exception(result.Errors.First().Description);
          }

          Log.Debug("bob created");
        }
        else
        {
          Log.Debug("bob already exists");
        }
      }
    }
  }
}
