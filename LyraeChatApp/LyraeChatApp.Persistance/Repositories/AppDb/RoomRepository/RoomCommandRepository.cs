using LyraeChatApp.Domain.Models.Room;
using LyraeChatApp.Domain.Repositories.App.RoomRepositories;
using System.Data.SqlClient;

namespace LyraeChatApp.Persistance.Repositories.AppDb.RoomRepository;

public class RoomCommandRepository : Repository,IRoomCommandRepository
{
    public RoomCommandRepository(SqlConnection context, SqlTransaction transaction)
    {
        this._context= context;
        this._transaction= transaction;
    }
    public async Task AddAsync(Room model)
    {
        var query = "INSERT INTO Room (Name, CreatedDate, CreatorName,  IsActive) " +
                "VALUES (@name, @createddate, @creatorname, @isactive);" +
                "SELECT SCOPE_IDENTITY()";
        var command = CreateCommand(query);
        command.Parameters.AddWithValue("@name", model.Name);
        command.Parameters.AddWithValue("@createddate", model.CreatedDate);
        command.Parameters.AddWithValue("@creatorname", model.CreatorName);
        command.Parameters.AddWithValue("@isactive", model.IsActive);
        await command.ExecuteNonQueryAsync();

    }

    public Task AddRangeAsync(IEnumerable<Room> model)
    {
        throw new NotImplementedException();
    }

    public void Remove(Room model)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveById(int id)
    {
        var command = CreateCommand("delete from Room where Id=@id");
        command.Parameters.AddWithValue("@id", id);
        await command.ExecuteNonQueryAsync();
    }

    public void RemoveRange(IEnumerable<Room> model)
    {
        throw new NotImplementedException();
    }

    public void Update(Room entity)
    {
        var query = "update Room set Name=@name, UpdaterName=@updaterName ,UpdateDate=@updateDate  where Id=@id";
        var command = CreateCommand(query);
        command.Parameters.AddWithValue("@name", entity.Name);
        command.Parameters.AddWithValue("@updaterName", entity.UpdaterName);
        command.Parameters.AddWithValue("@updateDate", entity.UpdateDate);
        command.Parameters.AddWithValue("@id", entity.Id);

        command.ExecuteNonQuery();
    }

    public void UpdateRange(IEnumerable<Room> model)
    {
        throw new NotImplementedException();
    }
}
