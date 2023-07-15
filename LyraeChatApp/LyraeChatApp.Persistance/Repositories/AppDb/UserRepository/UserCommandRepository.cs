using LyraeChatApp.Domain.Models.User;
using LyraeChatApp.Domain.Repositories.App.UserRepositories;
using System.Data.SqlClient;

namespace LyraeChatApp.Persistance.Repositories.AppDb.UserRepository;

public class UserCommandRepository : Repository, IUserCommandRepository
{
    public UserCommandRepository(SqlConnection context, SqlTransaction transaction)
    {
        this._context = context;
        this._transaction = transaction;
    }
    public async Task AddAsync(User model)
    {
        var query = "INSERT INTO Users " +
            "(UserName,Email,PhoneNumber," +
            "Name,SurName,Photo,DepartmanId," +
            "CreatedDate,CreatorName,DeletedDate," +
            "DeleterName,UpdateDate,UpdaterName,IsActive,PasswordHash,RoleName) " +
            "VALUES " +
            "(@username,@email,@phonenumber,@name,@surname," +
            "@photo, @departmanId,@createddate,@creatorname," +
            "@DeletedDate,@deletername,@updatedate,@updatername,@isactive,@passwordHash,@roleName); " +
            "SELECT SCOPE_IDENTITY();";
        var command = CreateCommand(query);
        command.Parameters.AddWithValue("@username", model.UserName);
        command.Parameters.AddWithValue("@email", model.Email);
        command.Parameters.AddWithValue("@phonenumber", model.PhoneNumber);
        command.Parameters.AddWithValue("@name", model.Name);
        command.Parameters.AddWithValue("@surname", model.SurName);
        command.Parameters.AddWithValue("@photo", model.Photo);
        command.Parameters.AddWithValue("@departmanId", model.DepartmanId);
        command.Parameters.AddWithValue("@createddate", model.CreatedDate);
        command.Parameters.AddWithValue("@creatorname", model.CreatorName);
        command.Parameters.AddWithValue("@DeletedDate", model.DeletedTime == null ? DBNull.Value : model.DeletedTime);
        command.Parameters.AddWithValue("@deletername", model.DeleterName == null ? DBNull.Value : model.DeleterName);
        command.Parameters.AddWithValue("@updatedate", model.UpdateDate == null ? DBNull.Value : model.UpdateDate);
        command.Parameters.AddWithValue("@updatername", model.UpdaterName == null ? DBNull.Value : model.UpdaterName);
        command.Parameters.AddWithValue("@isactive", model.IsActive);
        command.Parameters.AddWithValue("@passwordHash", model.PasswordHash);
        command.Parameters.AddWithValue("@roleName", model.RoleName);
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

    public async Task RemoveById(int id)
    {
        var command = CreateCommand("delete from Users where Id=@id");
        command.Parameters.AddWithValue("@id", id);
        await command.ExecuteNonQueryAsync();
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

    public void UpdatePassword(User entity)
    {
        var query = "update Users set PasswordHash=@pass where Email=@email";
        var command = CreateCommand(query);
        command.Parameters.AddWithValue("@pass", entity.PasswordHash);
        command.Parameters.AddWithValue("@email", entity.Email);

        command.ExecuteNonQuery();
    }

    public void UpdateRange(IEnumerable<User> model)
    {
        throw new NotImplementedException();
    }
}
