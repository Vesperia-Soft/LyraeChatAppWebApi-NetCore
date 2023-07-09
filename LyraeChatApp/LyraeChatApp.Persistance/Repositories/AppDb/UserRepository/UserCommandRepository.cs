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
    public Task AddAsync(User model)
    {
        throw new NotImplementedException();
    }

    public Task AddRangeAsync(IEnumerable<User> model)
    {
        throw new NotImplementedException();
    }

    public void Remove(User entity)
    {
        throw new NotImplementedException();
    }

    public Task RemoveById(string id)
    {
        throw new NotImplementedException();
    }

    public void RemoveRange(IEnumerable<User> model)
    {
        throw new NotImplementedException();
    }

    public void Update(User entity)
    {
        throw new NotImplementedException();
    }

    public void UpdateRange(IEnumerable<User> model)
    {
        throw new NotImplementedException();
    }
}
