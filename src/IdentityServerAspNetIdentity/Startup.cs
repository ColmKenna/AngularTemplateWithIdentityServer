// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Linq;
using System.Reflection;
using Duende.IdentityServer;
using IdentityServerAspNetIdentity.Data;
using IdentityServerHost.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;


namespace IdentityServerAspNetIdentity
{
  public class Startup
  {
    public IWebHostEnvironment Environment { get; }
    public IConfiguration Configuration { get; }

    public Startup(IWebHostEnvironment environment, IConfiguration configuration)
    {
      Environment = environment;
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllersWithViews();

      //services.AddDbContext<ApplicationDbContext>(options =>
      //    options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"), 
      //        o => o.MigrationsAssembly(typeof(Startup).Assembly.FullName)));

      var connectionString = Configuration.GetConnectionString("DefaultConnection");
      services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));

      services.AddIdentity<ApplicationUser, IdentityRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();



      var migrationsAssembly = typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name;

      var builder = services.AddIdentityServer(options =>
      {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;

        // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
        options.EmitStaticAudienceClaim = true;
      })
          .AddAspNetIdentity<ApplicationUser>()
          .AddConfigurationStore(options =>
          {
            options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
              sql => sql.MigrationsAssembly(migrationsAssembly));
          })
          .AddOperationalStore(options =>
          {
            options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
              sql => sql.MigrationsAssembly(migrationsAssembly));
          });
      ;

      services.AddAuthentication()
        //.AddGoogle(options =>
        //{
        //  options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

        //  // register your IdentityServer with Google at https://console.developers.google.com
        //  // enable the Google+ API
        //  // set the redirect URI to https://localhost:5001/signin-google
        //  options.ClientId = "copy client ID from Google here";
        //  options.ClientSecret = "copy client secret from Google here";
        //})
        ;
    }




    public void Configure(IApplicationBuilder app)
    {
      SeedData.EnsureSeedData(app.ApplicationServices);
      if (Environment.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
      }

      app.UseStaticFiles();

      app.UseRouting();
      app.UseIdentityServer();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapDefaultControllerRoute();
      });
    }
  }
}