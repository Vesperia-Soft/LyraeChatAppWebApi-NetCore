using LyraeChatApp.Domain.Models;
using System.Collections.Generic;

namespace LyraeChatApp.Domain.Repositories.App.UserRepositories;

public interface IUserQueryRepository
{
    IQueryable<User> GetAll();
    IQueryable<User> GetWhere();
    Task<User> GetById(int Id);
    Task<User> GetFirstByExpression();
    Task<User> GetFirst();
}
