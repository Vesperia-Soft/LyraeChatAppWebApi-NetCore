using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.User;
using System.Collections.Generic;

namespace LyraeChatApp.Domain.Repositories.App.UserRepositories;

public interface IUserQueryRepository
{
    PaginationHelper<UserListModel> GetAll(int pageNumber, int pageSize);
    Task<User> CheckUserNameAndPassword(string userName);
    Task<bool> CheckDatabaseForUserName(string userName);
    Task<User> GetById(int Id);
    Task<User> GetFirstByExpression();
    Task<bool> CheckUserId(int userId);
    Task<User> GetFirst();
}
