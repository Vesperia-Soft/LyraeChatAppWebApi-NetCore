using LyraeChatApp.Domain.Models.User;

namespace LyraeChatApp.Application.Services;

public  interface IAuthService
{
    Task<User> CheckByUser(string userName);
    Task<bool> CheckDatabaseForUser(string userName);
    Task CreateUsers(CreateUserModel userModel);
}
