using LyraeChatApp.Domain.Models.Room;
using LyraeChatApp.Domain.Models.User;

namespace LyraeChatApp.Domain.Repositories.App.RoomRepositories;

public interface IRoomCommandRepository
{
    Task AddAsync(Room model);
    Task AddRangeAsync(IEnumerable<Room> model);
    void Update(Room entity);
    void UpdateRange(IEnumerable<Room> model);
    Task RemoveById(int id);
    void Remove(Room model);
    void RemoveRange(IEnumerable<Room> model);
}
