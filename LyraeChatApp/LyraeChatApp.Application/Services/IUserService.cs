using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.HelperModels;
using LyraeChatApp.Domain.Models.User;

namespace LyraeChatApp.Application.Services;

public interface IUserService
{
    Task<User> Get(int id);
    Task<User> CheckUser(string userName);
    PaginationHelper<UserListModel> GetAllUsers(PaginationRequest request);
    Task CreateUsers(CreateUserModel userModel);
    void UpdateUsers(User user);
    Task RemoveUsers(int id);


}
