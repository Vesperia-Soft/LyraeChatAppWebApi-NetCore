using LyraeChatApp.Domain.Models.Message;

namespace LyraeChatApp.Domain.Repositories.App.MessageRepositories;

public interface IMessageCommandRepository
{
    Task AddAsync(Message model);
    Task AddRangeAsync(IEnumerable<Message> model);
    void Update(Message entity);
    void UpdateRange(IEnumerable<Message> model);
    Task RemoveById(int id);
    void Remove(Message entity);
    void RemoveRange(IEnumerable<Message> model);
}
