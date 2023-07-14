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

    public async  Task<bool> CheckUserId(int userId)
    {
        var command = CreateCommand("SELECT COUNT(*) FROM Users WHERE Id = @id");

        command.Parameters.AddWithValue("@id", userId);

        var result = await command.ExecuteScalarAsync();

        int count;
        if (int.TryParse(result?.ToString(), out count))
        {
            return count > 0;
        }

        return false;
    }

    public PaginationHelper<UserListModel> GetAll(int pageNumber, int pageSize)
    {
        var command = CreateCommand("SELECT COUNT(*) FROM Users");
        int totalCount = (int)command.ExecuteScalar();

        command.CommandText = $"SELECT * FROM Users ORDER BY Id OFFSET {((pageNumber - 1) * pageSize)} ROWS FETCH NEXT {pageSize} ROWS ONLY";
        using (var reader = command.ExecuteReader())
        {
            List<UserListModel> users = new List<UserListModel>();
            while (reader.Read())
            {
                UserListModel user = new UserListModel();

                user.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                user.UserName = reader["UserName"] != DBNull.Value ? reader["UserName"].ToString() : string.Empty;
                user.Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty;
                user.PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? reader["PhoneNumber"].ToString() : string.Empty;
                user.Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : string.Empty;
                user.SurName = reader["SurName"] != DBNull.Value ? reader["SurName"].ToString() : string.Empty;
                user.Photo = reader["Photo"] != DBNull.Value ? reader["Photo"].ToString() : string.Empty;
                user.DepartmanId = reader["DepartmanId"] != DBNull.Value ? Convert.ToInt32(reader["DepartmanId"]) : 0;
                user.IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"]) : false;

                users.Add(user);
            }

            return new PaginationHelper<UserListModel>(totalCount, pageSize, pageNumber, users);
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
               Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
            UserName = reader["UserName"] != DBNull.Value ? reader["UserName"].ToString() : string.Empty,
           Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty,
           PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? reader["PhoneNumber"].ToString() : string.Empty,
           Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : string.Empty,
           SurName = reader["SurName"] != DBNull.Value ? reader["SurName"].ToString() : string.Empty,
           Photo = reader["Photo"] != DBNull.Value ? reader["Photo"].ToString() : string.Empty,
           DepartmanId = reader["DepartmanId"] != DBNull.Value ? Convert.ToInt32(reader["DepartmanId"]) : 0,
           IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"]) : false

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

    public async Task<User> CheckUserNameAndPassword(string userName)
    {
        var command = CreateCommand("SELECT *  FROM Users WHERE UserName = @UserName");

        command.Parameters.AddWithValue("@UserName", userName);

        using (var reader = command.ExecuteReader())
        {
            if (reader.Read())
            {
                var user = new User
                {
                    UserName = reader["UserName"] != DBNull.Value ? reader["UserName"].ToString() : string.Empty,
                    PasswordHash = reader["PasswordHash"] != DBNull.Value ? reader["PasswordHash"].ToString() : string.Empty,
                    RoleName = reader["RoleName"] != DBNull.Value ? reader["RoleName"].ToString() : string.Empty
                };

                return user;
            }
        }

        return null;
    }

    public async Task<bool> CheckDatabaseForUserName(string userName)
    {
        var command = CreateCommand("SELECT COUNT(*) FROM Users WHERE UserName = @userName");

        command.Parameters.AddWithValue("@userName", userName);
        var result = await command.ExecuteScalarAsync();

        int count;
        if (int.TryParse(result?.ToString(), out count))
        {
            return count > 0;
        }

        return false;
    }

    public async Task<User> GetByMail(string email)
    {
        var command = CreateCommand("SELECT * FROM Users  WHERE Email =@mail");

        command.Parameters.AddWithValue("@mail", email);

        using (var reader = command.ExecuteReader())
        {
            reader.Read();

            return new User
            {
                Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                UserName = reader["UserName"] != DBNull.Value ? reader["UserName"].ToString() : string.Empty,
                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty,
                PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? reader["PhoneNumber"].ToString() : string.Empty,
                Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : string.Empty,
                SurName = reader["SurName"] != DBNull.Value ? reader["SurName"].ToString() : string.Empty,
                Photo = reader["Photo"] != DBNull.Value ? reader["Photo"].ToString() : string.Empty,
                DepartmanId = reader["DepartmanId"] != DBNull.Value ? Convert.ToInt32(reader["DepartmanId"]) : 0,
                IsActive = reader["IsActive"] != DBNull.Value ? Convert.ToBoolean(reader["IsActive"]) : false

            };
        }
    }
}
