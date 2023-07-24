using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.HelperModels;
using LyraeChatApp.Domain.Models.Room;
using LyraeChatApp.Domain.ResponseDtosModel;

namespace LyraeChatApp.Application.Services;

public interface IRoomService
{
    Task<ResponseDto<RoomModel>> Get(int id);
    Task<ResponseDto<PaginationHelper<RoomListModel>>> GetAllRoom(PaginationRequest request);
    Task<int> CreateRoom(CreateRoomModel model);
    void UpdateRoom(UpdateRoomModel model);
    Task RemoveRoom(int id);
}
