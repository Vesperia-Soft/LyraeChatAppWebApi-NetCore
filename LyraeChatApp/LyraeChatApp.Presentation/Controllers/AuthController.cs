
using LyraeChatApp.Domain.Models.User;
using LyraeChatApp.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LyraeChatApp.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        public static User user = new User();

        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("[action]")]
        public IActionResult Register([FromBody] UserDto request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.UserName = request.UserName;
            user.PasswordHash = passwordHash;
            return Ok(user);
        }

        [HttpPost("[action]")]
        public IActionResult Login([FromBody] UserDto request)
        {
           if(user.UserName != request.UserName )
            {
                return BadRequest("User Not Found");
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wron Password");
            }
            string token = CreateToken(user);


            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim> 
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role,"User"),
                 new Claim(ClaimTypes.Role, "Admin"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires : DateTime.Now.AddDays(1),
                signingCredentials : creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;


        }

    }
}
