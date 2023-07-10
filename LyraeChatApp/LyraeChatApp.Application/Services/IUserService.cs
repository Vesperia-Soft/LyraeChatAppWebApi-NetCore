using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.HelperModels;
using LyraeChatApp.Domain.Models.User;

namespace LyraeChatApp.Application.Services;

public interface IUserService
{
    User Get(int id);
    PaginationHelper<User> GetAllUsers(PaginationRequest request);
    Task CreateUsers(User user);
    void UpdateUsers(User user);
    Task RemoveUsers(int id);

}
