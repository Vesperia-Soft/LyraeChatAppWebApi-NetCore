using LyraeChatApp.Domain.Models.LogModels;

namespace LyraeChatApp.Domain.Repositories.App.LogRepositories;

public interface ILogCommandRepository
{
    Task AddAsync(LogModel model);
}
