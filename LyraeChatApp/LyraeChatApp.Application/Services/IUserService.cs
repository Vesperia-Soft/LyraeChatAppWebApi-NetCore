using LyraeChatApp.Domain.Models;

namespace LyraeChatApp.Application.Services;

public interface IUserService
{
    User Get(int id);
    IQueryable<User> GetAllUsers();

}
