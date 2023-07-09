using LyraeChatApp.Domain.Models;
using LyraeChatApp.Domain.Repositories.App.UserRepositories;
using System.Data.SqlClient;

namespace LyraeChatApp.Persistance.Repositories.AppDb.UserRepository;

public class UserCommandRepository : Repository, IUserCommandRepository
{
    public UserCommandRepository(SqlConnection context , SqlTransaction transaction)
    {
        this._context = context;
        this._transaction = transaction;
    }
    public async Task AddAsync(User model)
    {
        var query = "INSERT INTO Users (Name) VALUES (@name); SELECT SCOPE_IDENTITY();";
        var command =CreateCommand(query);
        command.Parameters.AddWithValue("@name", model.Name);
        await command.ExecuteNonQueryAsync();
    }

    public Task AddRangeAsync(IEnumerable<User> model)
    {
        throw new NotImplementedException();
    }

    public void Remove(User user)
    {
        throw new NotImplementedException();
       
    }

    public async Task  RemoveById(int id)
    {
        var command = CreateCommand("delete from Users where Id=@id");
        command.Parameters.AddWithValue("@id", id);
          await  command.ExecuteNonQueryAsync();
    }

    public void RemoveRange(IEnumerable<User> model)
    {
        throw new NotImplementedException();
    }

    public void Update(User entity)
    {
        var query = "update Users set Name=@name where Id=@id";
        var command = CreateCommand(query);
        command.Parameters.AddWithValue("@name", entity.Name);
        command.Parameters.AddWithValue("@id", entity.Id);
        
        command.ExecuteNonQuery();
    }

    public void UpdateRange(IEnumerable<User> model)
    {
        throw new NotImplementedException();
    }
}
