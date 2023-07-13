
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
    private readonly IUserService _userService;

    public AuthController(IConfiguration configuration, IUserService userService)
    {
        _configuration = configuration;
        _userService = userService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromBody] CreateUserModel request)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.PasswordHash);
        request.PasswordHash = passwordHash;
       var checkUserName = await _userService.CheckUserName(request.UserName);
        if (checkUserName == true)
        {
            return BadRequest("There is an error in the sent data.");
        }
       await  _userService.CreateUsers(request);

        return Ok("Kayıt Başarılı") ;
    }

    [HttpPost("[action]")]
public async Task<IActionResult> Login([FromBody] UserDto request)
{
   
    var checkUser = await _userService.CheckUser(request.UserName);

    if (checkUser == null)
    {
        return BadRequest("User Not Found");
    }
    if (!BCrypt.Net.BCrypt.Verify(request.Password, checkUser.PasswordHash))
    {
        return BadRequest("Wrong Password");
    }

    string token = CreateToken(checkUser);

    return Ok(token);
}
    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.Role,user.RoleName),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Token").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;


    }

}
