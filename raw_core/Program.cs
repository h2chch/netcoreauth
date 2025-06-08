using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication("cookie")
    .AddCookie("cookie");
var app = builder.Build();

app.UseAuthentication();

app.MapGet("/username", (HttpContext ctx) =>
{
    return ctx.User.FindFirst("user").Value;

});

app.MapGet("/login", async (HttpContext ctx) =>
{
    var claims = new List<Claim>
    {
        new Claim("user", "hendrah"),
    };

    var identity = new ClaimsIdentity(claims, "cookie");
    var principal = new ClaimsPrincipal(identity);
    await ctx.SignInAsync("cookie", principal);

    return "OK";
});


app.Run();
