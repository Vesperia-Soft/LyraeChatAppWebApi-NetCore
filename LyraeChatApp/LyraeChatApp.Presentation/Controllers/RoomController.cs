using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Models.Department;
using LyraeChatApp.Domain.Models.HelperModels;
using LyraeChatApp.Domain.Models.Room;
using LyraeChatApp.Persistance.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LyraeChatApp.Presentation.Controllers;
[ApiController]
[Route("[controller]")]
public class RoomController :ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet("[action]")]
    public IActionResult GetAll([FromQuery] PaginationRequest request)
    {
        var rooms = _roomService.GetAllRoom(request);
        return Ok(rooms);
    }
    [HttpGet("{id}")]
    public ActionResult<IEnumerable<string>> Get(int id)
    {
        return Ok(
            _roomService.Get(id)
        );
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create(CreateRoomModel model)
    {
      var roomId=  await _roomService.CreateRoom(model);
        return Ok(roomId);
    }

    [HttpPut("[action]")]
    public IActionResult Update(UpdateRoomModel room)
    {
        _roomService.UpdateRoom(room);
        return Ok();
    }

    [HttpDelete("(id)")]
    public IActionResult Remove(int id)
    {
        _roomService.RemoveRoom(id);
        return Ok();
    }

}
