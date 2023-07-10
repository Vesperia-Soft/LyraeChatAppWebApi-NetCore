using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Models.HelperModels;
using LyraeChatApp.Domain.Models.User;
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

    [HttpPost("[action]")]
    public async  Task<IActionResult> Create(User user)
    {
       await  _userService.CreateUsers(user);
        return Ok(user);
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
    
}
