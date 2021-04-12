using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace MvcClient.Controllers
{
  public class AccountController : Controller
  {
    public IActionResult LogOut()
    {
      return new SignOutResult(new[]
      {
        CookieAuthenticationDefaults.AuthenticationScheme,
        OpenIdConnectDefaults.AuthenticationScheme
      });
    }

    public IActionResult LogIn() => Challenge(new AuthenticationProperties { RedirectUri = "/" });

  }
}