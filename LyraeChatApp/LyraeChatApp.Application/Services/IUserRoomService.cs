using LyraeChatApp.Domain.Models.UserRoom;
using LyraeChatApp.Domain.ResponseDtosModel;

namespace LyraeChatApp.Application.Services;

public interface IUserRoomService
{
    Task<ResponseDto<IList<UserRoomListModel>>> GetUserRoomListAsync(int userId);
    Task Create(IList<int> userId, int roomId);
}
