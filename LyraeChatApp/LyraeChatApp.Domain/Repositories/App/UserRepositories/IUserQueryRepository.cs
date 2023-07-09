using LyraeChatApp.Domain.Models;
using LyraeChatApp.Domain.Repositories.GenericRepositories.AppDbContext;

namespace LyraeChatApp.Domain.Repositories.App.UserRepositories;

public interface IUserQueryRepository : IAppQueryRepository<User>
{
}
