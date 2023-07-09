using LyraeChatApp.Application.Services;
using LyraeChatApp.Persistance.Service;
using Microsoft.AspNetCore.Mvc;

namespace LyraeChatApp.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController :ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public ActionResult<IEnumerable<string>> Get(int id)
    {
        return Ok(
            _userService.Get(id)
        );
    }
}
