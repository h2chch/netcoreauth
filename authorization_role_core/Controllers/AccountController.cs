
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
public class AccountController : ControllerBase
{
    // if not using ApiController, you can inject SignInManager directly in the constructor
    //SignInManager<IdentityUser> _signInManager;
    //public AccountController(SignInManager<IdentityUser> signInManager)
    //{
    //    _signInManager = signInManager;
    //}

    [HttpGet("/login")]
    public async Task<IActionResult> Login(SignInManager<IdentityUser> signInManager)
    {
        await signInManager.PasswordSignInAsync("user", "password", false, false);

        return Ok();
    }
}