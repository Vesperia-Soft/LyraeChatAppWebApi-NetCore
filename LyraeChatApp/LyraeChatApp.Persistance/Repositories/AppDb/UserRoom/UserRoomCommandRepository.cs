using LyraeChatApp.Domain.Repositories.App.UserRoom;
using System.Data.SqlClient;

namespace LyraeChatApp.Persistance.Repositories.AppDb.UserRoom;

public class UserRoomCommandRepository : Repository, IUserRoomCommandRepository
{
    public UserRoomCommandRepository(SqlConnection context, SqlTransaction transaction)
    {
        _context = context;
        _transaction = transaction;
    }

    public async Task AddAsync(Domain.Models.UserRoom.UserRoom model)
    {
        var query = "INSERT INTO UserRoom(UserId, RoomId) VALUES (@userId, @roomId)";

        var command = CreateCommand(query);
        command.Parameters.AddWithValue("@userId", model.UserId);
        command.Parameters.AddWithValue("@roomId", model.RoomId);

        await command.ExecuteNonQueryAsync();
    }
}
