using LyraeChatApp.Domain.Models.UserRoom;
using LyraeChatApp.Domain.Repositories.App.UserRoom;
using System.Data.SqlClient;

namespace LyraeChatApp.Persistance.Repositories.AppDb.UserRoom;

public class UserRoomQueryRepository : Repository, IUserRoomQueryRepository
{
    public UserRoomQueryRepository(SqlConnection context, SqlTransaction transaction)
    {
        _context = context;
        _transaction = transaction;
    }
    public IList<UserRoomListModel> GetAllByUser(int userId)
    {
        var command = CreateCommand("SELECT *  FROM UserRoom where UserId=@userId");
        command.Parameters.AddWithValue("userId", userId);

        using (var reader = command.ExecuteReader())
        {
            List<UserRoomListModel> userRooms = new List<UserRoomListModel>();
            while (reader.Read())
            {
                UserRoomListModel userRoom = new UserRoomListModel();

                userRoom.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                userRoom.UserId = reader["UserId"] != DBNull.Value ? Convert.ToInt32(reader["UserId"]) : 0;
                userRoom.RoomId = reader["RoomId"] != DBNull.Value ? Convert.ToInt32(reader["RoomId"]) : 0;

                userRooms.Add(userRoom);
            }

            return userRooms;
        }
    }
}
