using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AuthorizationRole.Controllers;

[ApiController]
public class HomeControllers : ControllerBase
{
    [HttpGet("/")]
    public string Index() => "Hello World";

    [HttpGet("/secret")]
    [Authorize(Roles = "admin")]
    public string Secret() => "This is a secret page";
}
