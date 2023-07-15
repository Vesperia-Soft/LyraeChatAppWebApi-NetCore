using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.Department;
using LyraeChatApp.Domain.Models.Room;
using LyraeChatApp.Domain.Repositories.App.RoomRepositories;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace LyraeChatApp.Persistance.Repositories.AppDb.RoomRepository;

public class RoomQueryRepository : Repository,IRoomQueryRepository
{

    public RoomQueryRepository(SqlConnection context, SqlTransaction transaction)
    {
        this._context = context;
        this._transaction = transaction;
    }
    public async  Task<bool> CheckRoomId(int roomId)
    {
        var command = CreateCommand("SELECT COUNT(*) FROM Room WHERE Id = @id");
        command.Parameters.AddWithValue("@id", roomId);

        var result = await command.ExecuteScalarAsync();

        int count;
        if (int.TryParse(result?.ToString(), out count))
        {
            return count > 0;
        }

        return false;
    }

    public PaginationHelper<RoomListModel> GetAll(int pageNumber, int pageSize)
    {
        var command = CreateCommand("SELECT COUNT(*) FROM Room");
        int totalCount = (int)command.ExecuteScalar();

        command.CommandText = $"SELECT * FROM Room ORDER BY Id OFFSET {((pageNumber - 1) * pageSize)} ROWS FETCH NEXT {pageSize} ROWS ONLY";
        using (var reader = command.ExecuteReader())
        {
            List<RoomListModel> rooms = new List<RoomListModel>();
            while (reader.Read())
            {
                rooms.Add(new RoomListModel
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString()
                });
            }

            return new PaginationHelper<RoomListModel>(totalCount, pageSize, pageNumber, rooms);
        }
    }

    public async Task<Room> GetById(int Id)
    {
        var command = CreateCommand("SELECT * FROM Room WHERE Id =@id");

        command.Parameters.AddWithValue("@id", Id);

        using (var reader = command.ExecuteReader())
        {
            reader.Read();

            return new Room
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString()
            };
        }
    }

    public async Task<int> GetByName(string name)
    {
        var command = CreateCommand("SELECT Id FROM Room WHERE Name = @name");
        command.Parameters.AddWithValue("@name", name);

        var result = await command.ExecuteScalarAsync();

        int id;
        if (int.TryParse(result?.ToString(), out id))
        {
            return id;
        }

        return 0;
    }

    public Task<Room> GetFirst()
    {
        throw new NotImplementedException();
    }

    public Task<Room> GetFirstByExpression()
    {
        throw new NotImplementedException();
    }

    public IQueryable<Room> GetWhere()
    {
        throw new NotImplementedException();
    }
}
