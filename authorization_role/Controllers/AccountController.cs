using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;


namespace AuthorizationRole.Controllers;

[ApiController]
public class AccountController : ControllerBase
{

    [HttpGet("/login")]
    public IActionResult Login()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new Claim("my_role", "admin")
        };
        var identity = new ClaimsIdentity(claims, "cookie", null, "my_role");
        var principal = new ClaimsPrincipal(identity);
        return SignIn(principal, authenticationScheme: "cookie");
    }

}