using LyraeChatApp.Domain.Models;
using LyraeChatApp.Domain.Repositories.App.UserRepositories;
using System.Data.SqlClient;

namespace LyraeChatApp.Persistance.Repositories.AppDb.UserRepository;

public class UserQueryRepository : Repository, IUserQueryRepository
{
    public UserQueryRepository(SqlConnection context, SqlTransaction transaction)
    {
        this._context = context;
        this._transaction = transaction;
    }
    public IQueryable<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetById(int Id)
    {
        var command = CreateCommand("SELECT * FROM Users  WHERE Id =@id");

        command.Parameters.AddWithValue("@id", Id);

        using (var reader = command.ExecuteReader())
        {
            reader.Read();

            return new User
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString()
            };
        }
    }

    public Task<User> GetFirst()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetFirstByExpression()
    {
        throw new NotImplementedException();
    }

    public IQueryable<User> GetWhere()
    {
        throw new NotImplementedException();
    }
}
