using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.User;
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
    public PaginationHelper<User> GetAll(int pageNumber, int pageSize)
    {
        var command = CreateCommand("SELECT COUNT(*) FROM Users");
        int totalCount = (int)command.ExecuteScalar();

        command.CommandText = $"SELECT * FROM Users ORDER BY Id OFFSET {((pageNumber - 1) * pageSize)} ROWS FETCH NEXT {pageSize} ROWS ONLY";
        using (var reader = command.ExecuteReader())
        {
            List<User> users = new List<User>();
            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString()
                });
            }

            return new PaginationHelper<User>(totalCount, pageSize, pageNumber, users);
        }
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
