using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlazorClient.MessageHandlers;
using BlazorClient.Services;

namespace BlazorClient
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var builder = WebAssemblyHostBuilder.CreateDefault(args);
      builder.RootComponents.Add<App>("#app");

      builder.Services
             .AddTransient<ApiAuthorizationMessageHandler>();

      builder.Services.AddOidcAuthentication(options =>
      {
        builder.Configuration.Bind("OidcConfiguration", options.ProviderOptions);
        builder.Configuration.Bind("UserOptions", options.UserOptions);
      });

      builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
      builder.Services.AddHttpClient<IWeatherForcastService, WeatherForcastService>(
               client => client.BaseAddress = new Uri("https://localhost:6001/"))
             .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

      builder.Services.AddHttpClient<IProductService, ProductService>(
               client => client.BaseAddress = new Uri("https://localhost:6001/"))
             .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();


      await builder.Build().RunAsync();
    }
  }
}
