using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Models.HelperModels;
using LyraeChatApp.Domain.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LyraeChatApp.Presentation.Controllers;
[Authorize(Roles = "Admin")]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpGet("[action]")]
    public IActionResult GetAll([FromQuery] PaginationRequest request)
    {
        var users = _userService.GetAllUsers(request);
        return Ok(users);
    }

    [HttpGet("{id}")]
    public ActionResult<IEnumerable<string>> Get(int id)
    {
      
        return Ok(
            _userService.Get(id)
        );
    }


    [HttpPut("[action]")]
    public IActionResult Update(User user)
    {
        _userService.UpdateUsers(user);
        return Ok();
    }

    [HttpDelete("(id)")]
    public IActionResult RemoveUsers(int id)
    {
        _userService.RemoveUsers(id);
        return Ok();
    }

    #region Helpers
    [NonAction]
    public IActionResult GetImage(string fileName)
    {
        string path = "./Content/Images/" + fileName;
        byte[] fileBytes = System.IO.File.ReadAllBytes(path);

        string mimeType = GetMimeType(fileName); 

        return File(fileBytes, mimeType);
    }

    private string GetMimeType(string fileName)
    {
        string ext = Path.GetExtension(fileName);

        switch (ext.ToLower())
        {
            case ".jpg":
            case ".jpeg":
                return "image/jpeg";
            case ".png":
                return "image/png";
            case ".gif":
                return "image/gif";
            default:
                return "application/octet-stream";
        }
    }
    #endregion

}
