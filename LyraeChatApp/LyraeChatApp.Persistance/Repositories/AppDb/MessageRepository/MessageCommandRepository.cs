using LyraeChatApp.Domain.Models.Message;
using LyraeChatApp.Domain.Repositories.App.MessageRepositories;
using System.Data.SqlClient;

namespace LyraeChatApp.Persistance.Repositories.AppDb.MessageRepository;

public class MessageCommandRepository : Repository, IMessageCommandRepository
{

    public MessageCommandRepository(SqlConnection context, SqlTransaction transaction)
    {
        this._context = context;
        this._transaction = transaction;
    }
    public async Task AddAsync(Message model)
    {
        var query = "INSERT INTO Messages " +
            "(Text,UserId,RoomId,TimeStamps,CreatedDate,CreatorName,DeletedDate," +
            "DeleterName,UpdateDate,UpdaterName,IsActive) " +
            "VALUES " +
            "(@text,@userid,@roomid,@timestamps, @createddate,@creatorname," +
            "@deletedDate,@deletername,@updatedate,@updatername,@isactive); " +
            "SELECT SCOPE_IDENTITY();";
        var command = CreateCommand(query);
        command.Parameters.AddWithValue("@text", model.Text);
        command.Parameters.AddWithValue("@userid", model.UserId);
        command.Parameters.AddWithValue("@roomid", model.RoomId);
        command.Parameters.AddWithValue("@timestamps", DateTime.Now);
        command.Parameters.AddWithValue("@createddate", DateTime.Now);
        command.Parameters.AddWithValue("@creatorname", model.CreatorName);
        command.Parameters.AddWithValue("@deletedDate", model.DeletedTime == null ? DBNull.Value : model.DeletedTime);
        command.Parameters.AddWithValue("@deletername", model.DeleterName == null ? DBNull.Value : model.DeleterName);
        command.Parameters.AddWithValue("@updatedate", model.UpdateDate == null ? DBNull.Value : model.UpdateDate);
        command.Parameters.AddWithValue("@updatername", model.UpdaterName == null ? DBNull.Value : model.UpdaterName);
        command.Parameters.AddWithValue("@isactive", model.IsActive);
        await command.ExecuteNonQueryAsync();
    }

    public Task AddRangeAsync(IEnumerable<Message> model)
    {
        throw new NotImplementedException();
    }

    public void Remove(Message entity)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveById(int id)
    {
        var command = CreateCommand("delete from Messages where Id=@id");
        command.Parameters.AddWithValue("@id", id);
        await command.ExecuteNonQueryAsync();
    }

    public void RemoveRange(IEnumerable<Message> model)
    {
        throw new NotImplementedException();
    }

    public void Update(Message entity)
    {
        var query = "update Messages set Text=@text,UserId=@userid,RoomId=@roomid where Id=@id";
        var command = CreateCommand(query);
        command.Parameters.AddWithValue("@text", entity.Text);
        command.Parameters.AddWithValue("@userid", entity.UserId);
        command.Parameters.AddWithValue("@roomid", entity.RoomId);
        command.Parameters.AddWithValue("@id", entity.Id);

        command.ExecuteNonQuery();
    }

    public void UpdateRange(IEnumerable<Message> model)
    {
        throw new NotImplementedException();
    }
}
