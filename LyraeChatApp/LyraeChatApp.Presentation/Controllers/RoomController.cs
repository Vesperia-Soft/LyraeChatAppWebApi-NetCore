using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Models.HelperModels;
using LyraeChatApp.Domain.Models.Room;
using LyraeChatApp.Presentation.ControllerBased;
using Microsoft.AspNetCore.Mvc;

namespace LyraeChatApp.Presentation.Controllers;
[ApiController]
[Route("[controller]")]
public class RoomController : CustomBaseController
{
    #region Fields
    private readonly IRoomService _roomService;
    private readonly IUserRoomService _userRoomService;
    #endregion

    #region Ctor
    public RoomController(
        IRoomService roomService,
        IUserRoomService userRoomService
        )
    {
        _roomService = roomService;
        _userRoomService = userRoomService;
    }
    #endregion

    #region Methods

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationRequest request)
    {
        var rooms = await _roomService.GetAllRoom(request);

        return CreateActionResultInstance(rooms);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var room = await _roomService.Get(id);

        return CreateActionResultInstance(room);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create(CreateRoomModel model)
    {
        var roomId = await _roomService.CreateRoom(model);

       await  _userRoomService.Create(model.UserId, roomId);

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

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllUserRoom([FromQuery] int userId)
    {
      var result =   await _userRoomService.GetUserRoomListAsync(userId);

        return CreateActionResultInstance(result);
    }
    #endregion
}
