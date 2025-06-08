using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication("cookie")
    .AddCookie("cookie");

var app = builder.Build();

app.UseAuthentication();

app.MapGet("/admin", (HttpContext ctx) =>
{
    if (!ctx.User.Identities.Any(x => x.AuthenticationType == "cookie"))
    {
        ctx.Response.StatusCode = 401;
        return "Unauthorized";
    }

    if (!ctx.User.HasClaim("role", "admin"))
    {
        ctx.Response.StatusCode = 403;
        return "Forbidden";
    }

    return "OK";
});

app.MapGet("/portaladmin", (HttpContext ctx) =>
{
    if (!ctx.User.Identities.Any(x => x.AuthenticationType == "cookie"))
    {
        ctx.Response.StatusCode = 401;
        return "Unauthorized";
    }

    if (!ctx.User.HasClaim("role", "portal_admin"))
    {
        ctx.Response.StatusCode = 403;
        return "Forbidden";
    }

    return "OK";

});


app.MapGet("/login", async (HttpContext ctx) =>
{
    var claims = new List<Claim>
    {
        new Claim("user", "hendrah"),
        new Claim("role", "admin"),
    };
    var identity = new ClaimsIdentity(claims, "cookie");
    var principal = new ClaimsPrincipal(identity);

    await ctx.SignInAsync("cookie", principal);

    return "OK";
});


app.Run();
