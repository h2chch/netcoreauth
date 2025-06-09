
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class HomeController : ControllerBase
{
    [HttpGet("/")]
    public string Index() => "Welcome to page";

    [HttpGet("/secret")]
    [Authorize(Roles = "admin")]
    public string Secret() => "This is a secret page for admins only";
}