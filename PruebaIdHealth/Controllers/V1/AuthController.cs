using Microsoft.AspNetCore.Mvc;
using PruebaIdHealth.Services.Interfaces;
using PruebaIdHealth.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using PruebaIdHealth.Services;

namespace PruebaIdHealth.Controllers.V1;

[Controller]
[Route("api/v1/auth")]
public class AuthController : Controller
{

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Credential credentials)
    {
        string result = await _authService.LoginAsync(credentials);
        return Ok(new { success = true, token = result });
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> register([FromBody] User user)
    {
        await _authService.RegisterAsync(user);
        return Ok(new { success = true, message = "The user has been registered" });
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<User> users = await _authService.GetAsync();
        return Ok(users);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] User user)
    {
        await _authService.UpdateAsync(id, user);
        return Ok(new { success = true, message = "The user has been updated" });
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _authService.DeleteAsync(id);
        return Ok(new { success = true, message = "The user has been deleted" });
    }

}