using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.Department;
using LyraeChatApp.Domain.Models.HelperModels;
using LyraeChatApp.Domain.Models.Room;
using LyraeChatApp.Domain.Models.User;

namespace LyraeChatApp.Application.Services;

public interface IRoomService
{
    Task<Room> Get(int id);
    PaginationHelper<RoomListModel> GetAllRoom(PaginationRequest request);
    Task<int> CreateRoom(CreateRoomModel model);
    void UpdateRoom(UpdateRoomModel model);
    Task RemoveRoom(int id);
}
