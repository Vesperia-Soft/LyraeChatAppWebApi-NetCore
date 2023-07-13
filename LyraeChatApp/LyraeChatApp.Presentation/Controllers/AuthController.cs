
using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Models.User;
using LyraeChatApp.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LyraeChatApp.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    public static User user = new User();

    private readonly IConfiguration _configuration;
    private readonly IAuthService _authService;
    private readonly IJwtService _jwtService;

    public AuthController(IConfiguration configuration, IAuthService authService, IJwtService jwtService)
    {
        _configuration = configuration;
        _authService = authService;
        _jwtService = jwtService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromBody] CreateUserModel request)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.PasswordHash);
        request.PasswordHash = passwordHash;
        var checkUserName = await _authService.CheckDatabaseForUser(request.UserName);
        if (checkUserName == true)
        {
            return BadRequest("There is an error in the sent data.");
        }
        await _authService.CreateUsers(request);

        return Ok("Kayıt Başarılı");
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] UserDto request)
    {

        var checkUser = await _authService.CheckByUser(request.UserName);

        if (checkUser == null)
        {
            return BadRequest("User Not Found");
        }
        if (!BCrypt.Net.BCrypt.Verify(request.Password, checkUser.PasswordHash))
        {
            return BadRequest("Wrong Password");
        }

        string token = _jwtService.CreateToken(checkUser);

        return Ok(token);
    }
}
