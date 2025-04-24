using FormBuilder.API.Infrastructure.Authentication;
using FormBuilder.API.Models.Dto.AuthDtos;
using FormBuilder.Domain.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace FormBuilder.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager, 
    IJwtService jwtService): ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Authenticate([FromBody] LoginDto credentials)
    {
        var user = await userManager.FindByEmailAsync(credentials.Username);
         if (user == null) 
            return BadRequest(new { msg = "Username or password is incorrect" });

        var result = await signInManager.PasswordSignInAsync(user, credentials.Password, false, false);
        if (!result.Succeeded)
            return BadRequest(new {msg = "Username or password is incorrect"});

        var token = jwtService.GenerateToken(user);

        return Ok(new {token});
    }

    [HttpPost]
    public async Task<IActionResult> SignUp()
    {
        throw new NotImplementedException();
    }
}
