using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.User;
using System.Collections.Generic;

namespace LyraeChatApp.Domain.Repositories.App.UserRepositories;

public interface IUserQueryRepository
{
    PaginationHelper<User> GetAll(int pageNumber, int pageSize);
    IQueryable<User> GetWhere();
    Task<User> GetById(int Id);
    Task<User> GetFirstByExpression();
    Task<User> GetFirst();
}
