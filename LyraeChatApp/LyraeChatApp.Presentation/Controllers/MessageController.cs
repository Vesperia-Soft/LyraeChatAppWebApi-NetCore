using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Models.HelperModels;
using LyraeChatApp.Domain.Models.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LyraeChatApp.Presentation.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("[controller]")]
public class MessageController : Controller
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpGet("[action]")]
    public IActionResult GetAll([FromQuery] PaginationRequest request)
    {
        var users = _messageService.GetAll(request);
        return Ok(users);
    }

    [HttpGet("{id}")]
    public ActionResult<IEnumerable<string>> Get(int id)
    {
        return Ok(
            _messageService.Get(id)
        );
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create(CreateMessageModel model)
    {
        await _messageService.Create(model);
        return Ok(model);
    }

    [HttpPut("[action]")]
    public IActionResult Update(UpdateMessageModel message)
    {
        _messageService.Update(message);
        return Ok();
    }

    [HttpDelete("(id)")]
    public IActionResult Remove(int id)
    {
        _messageService.Remove(id);
        return Ok();
    }
}
