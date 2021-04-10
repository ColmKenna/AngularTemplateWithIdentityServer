using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorClient.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;


namespace BlazorClient.MessageHandlers
{
  public class ApiAuthorizationMessageHandler : AuthorizationMessageHandler
  {
    public ApiAuthorizationMessageHandler(IAccessTokenProvider provider, NavigationManager navigation) : base(provider, navigation)
    {
      ConfigureHandler(
        authorizedUrls: new[] { "https://localhost:6001/" });
    }


  }
}