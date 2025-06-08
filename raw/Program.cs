using Microsoft.AspNetCore.DataProtection;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDataProtection();
builder.Services.AddHttpContextAccessor();  
builder.Services.AddScoped<AuthService>();

var app = builder.Build();


app.Use((ctx, next) =>
{
    var authCookie = ctx.Request.Cookies["auth"];
    if (authCookie != null)
    {
        var dpp = ctx.RequestServices.GetRequiredService<IDataProtectionProvider>();
        var protector = dpp.CreateProtector("auth-cookie-protector");
        var payload = protector.Unprotect(authCookie);
        if (payload != null)
        {
            if (payload.Contains(":"))
            {
                var keyValue = payload.Split(':');

                var claims = new List<Claim>
                {
                    new Claim(keyValue[0], keyValue[1]),
                };

                var identity = new ClaimsIdentity(claims);
                var principal = new ClaimsPrincipal(identity);
                ctx.User = principal;
            }

        }
    }
    return next();
});

app.MapGet("/username", (HttpContext ctx) =>
{
    return ctx.User.FindFirst("user").Value;
    
});

app.MapGet("/login", (AuthService authService) =>
{
    authService.Login();
    return "OK";
});

app.UseCookiePolicy();
app.Run();


public class AuthService
{
    IDataProtectionProvider _dpp;
    IHttpContextAccessor _accessor;

    public AuthService(IDataProtectionProvider dpp, IHttpContextAccessor accessor)
    {
        _dpp = dpp;
        _accessor = accessor;
    }

    public void Login()
    {
        var protector = _dpp.CreateProtector("auth-cookie-protector");
        var userName = protector.Protect("user:hendrah");
        _accessor.HttpContext.Response.Cookies.Append("auth", userName);
    }
}