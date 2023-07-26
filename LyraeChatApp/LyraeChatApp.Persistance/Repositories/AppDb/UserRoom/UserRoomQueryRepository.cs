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
    public IList<UserRoomListModel> GetOtherUsersInSameRooms(int userId)
    {
        var otherUsers = new List<UserRoomListModel>();

        var commandGetOtherUsers = CreateCommand(@"
        SELECT DISTINCT u.Id, u.UserName, u.Name, u.SurName, u.Photo ,ur.RoomId
        FROM UserRoom ur
        JOIN Users u ON ur.UserId = u.Id
        WHERE ur.RoomId IN (SELECT RoomId FROM UserRoom WHERE UserId = @userId)
        AND ur.UserId != @userId
    ");
        commandGetOtherUsers.Parameters.AddWithValue("userId", userId);

        using (var reader = commandGetOtherUsers.ExecuteReader())
        {
            while (reader.Read())
            {
                var userRoom = new UserRoomListModel
                {
                    UserId = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                    RoomId = reader["RoomId"] != DBNull.Value ? Convert.ToInt32(reader["RoomId"]) : 0,
                    UserName = reader["UserName"] != DBNull.Value ? reader["UserName"].ToString() : "",
                    Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : "",
                    SurName = reader["SurName"] != DBNull.Value ? reader["SurName"].ToString() : "",
                    Photo = reader["Photo"] != DBNull.Value ? reader["Photo"].ToString() : ""
                };

                otherUsers.Add(userRoom);
            }
        }

        return otherUsers;
    }


}
