using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.Room;

namespace LyraeChatApp.Domain.Repositories.App.RoomRepositories;

public interface IRoomQueryRepository
{
    PaginationHelper<RoomListModel> GetAll(int pageNumber, int pageSize);
    IQueryable<Room> GetWhere();
    Task<Room> GetById(int Id);
    Task<Room> GetFirstByExpression();
    Task<bool> CheckRoomId(int roomId);
    Task<Room> GetFirst();
    Task<int> GetByName(string name);
}
