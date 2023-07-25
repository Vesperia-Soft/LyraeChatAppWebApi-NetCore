using LyraeChatApp.Domain.Models.UserRoom;

namespace LyraeChatApp.Domain.Repositories.App.UserRoom;

public interface IUserRoomQueryRepository
{
    IList<UserRoomListModel> GetAllByUser(int userId);
}
