using LyraeChatApp.Domain.Models.UserRoom;

namespace LyraeChatApp.Domain.Repositories.App.UserRoom;

public interface IUserRoomCommandRepository
{
    Task AddAsync(Domain.Models.UserRoom.UserRoom model);
}
