
using LyraeChatApp.Application.Services;
using LyraeChatApp.Application.Services.Utilities;
using LyraeChatApp.Domain.Models.User;
using LyraeChatApp.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace LyraeChatApp.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    #region Fields
    private readonly IConfiguration _configuration;
    private readonly IAuthService _authService;
    private readonly IJwtService _jwtService;
    public static User user = new User();
    #endregion

    #region Ctor
    public AuthController(
        IConfiguration configuration,
        IAuthService authService,
        IJwtService jwtService)
    {
        _configuration = configuration;
        _authService = authService;
        _jwtService = jwtService;
    }
    #endregion

    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromForm] CreateUserModel request)
    {
        string fileName = await UploadImage(request.Image);
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.PasswordHash);
        request.PasswordHash = passwordHash;
        request.Photo = fileName;
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

    [HttpPost("[action]")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
    {
        model.NewPassword = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
        await _authService.ChangePassword(model);
        return Ok();
    }

    #region Helpers
    [NonAction]
    public async Task<string> UploadImage(IFormFile image)
    {
        string fileFormat = image.FileName.Substring(image.FileName.LastIndexOf("."));
        fileFormat = fileFormat.ToLower();
        string filename = Guid.NewGuid().ToString() + fileFormat;
        string path = "./Content/Images/" + filename;

        using (var stream = System.IO.File.Create(path))
        {
            await image.CopyToAsync(stream);
        }

        return filename;
    }
    #endregion

}
