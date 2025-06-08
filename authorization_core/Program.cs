using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("cookie")
    .AddCookie("cookie");
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("admin", policy =>
    {
        policy.RequireAuthenticatedUser()
            .AddAuthenticationSchemes("cookie")
            .RequireClaim("role", "admin");
    });
    opt.AddPolicy("portal_admin", policy =>
    {
        policy.RequireAuthenticatedUser()
            .AddAuthenticationSchemes("cookie")
            .RequireClaim("role", "portal_admin");
    });
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/admin", (HttpContext ctx) =>
{
    return "OK";
}).RequireAuthorization("admin");

app.MapGet("/portaladmin", (HttpContext ctx) =>
{
    return "OK";
}).RequireAuthorization("portal_admin");


app.MapGet("/login", async (HttpContext ctx) =>
{
    var claims = new List<Claim>
    { 
        new Claim("user", "hendrah"),
        new Claim("role", "admin")
    };

    var identity = new ClaimsIdentity(claims, "cookie");

    var principal = new ClaimsPrincipal(identity);

    await ctx.SignInAsync("cookie", principal);

}).AllowAnonymous();

app.Run();
