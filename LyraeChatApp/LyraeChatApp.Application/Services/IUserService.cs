using LyraeChatApp.Domain.Models;

namespace LyraeChatApp.Application.Services;

public interface IUserService
{
    User Get(int id);
    IQueryable<User> GetAllUsers();
    Task CreateUsers(User user);
    void UpdateUsers(User user);
    Task RemoveUsers(int id);

}
