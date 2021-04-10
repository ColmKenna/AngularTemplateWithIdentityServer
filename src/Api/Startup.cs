﻿// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Api.Policies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Api
{

  public class Startup
  {
    
    public void ConfigureServices(IServiceCollection services)
    {
      //services.AddCors(options =>
      //{
      //  options.AddPolicy(name: MyAllowSpecificOrigins,
      //    builder =>
      //    {
      //      builder.WithOrigins("https://localhost:5003");
      //    });
      //});

      services.AddCors(options =>
      {
        // this defines a CORS policy called "default"
        options.AddPolicy("default", policy =>
        {
          policy.WithOrigins("https://localhost:5003", "https://localhost:5004")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
      });

      services.AddControllers();

      // accepts any access token issued by identity server
      services.AddAuthentication("Bearer")
              .AddJwtBearer("Bearer", options =>
              {
                options.Authority = "https://localhost:5001";

                options.TokenValidationParameters = new TokenValidationParameters
                {
                  ValidateAudience = false
                };
              });

      // adds an authorization policy to make sure the token is for scope 'api1'
      services.AddAuthorization(options =>
      {
        options.AddPolicy(CanAccessApi.ApiScope, CanAccessApi.Policy());
        options.AddPolicy(CanViewProductsPolicy.CanViewProducts, CanViewProductsPolicy.Policy() );
        
      });

    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseRouting();
      app.UseCors("default");
      //app.UseCors(MyAllowSpecificOrigins);
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers()
                 .RequireAuthorization("ApiScope");
      });
    }
  }

}
